using FluentAssertions;
using Moq;
using RentARide.Application.Common.Interfaces;
using RentARide.Application.DTOs.Rentals;
using RentARide.Application.Rentals.Commands.CreateRental;
using RentARide.Domain.Entities;
using RentARide.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq; 

namespace RentARide.Tests.Rentals
{
    public class CreateRentalCommandHandlerTests
    {
        private readonly Mock<IApplicationDbContext> _mockContext;
        private readonly Mock<IHolidayService> _mockHolidayService;
        private readonly CreateRentalCommandHandler _handler;

        public CreateRentalCommandHandlerTests()
        {
            _mockContext = new Mock<IApplicationDbContext>();
            _mockHolidayService = new Mock<IHolidayService>();
            _handler = new CreateRentalCommandHandler(_mockContext.Object, _mockHolidayService.Object);
        }

        [Fact]
        public async Task Should_Return_Failure_When_Vehicle_Already_Booked()
        {
            
            var request = new CreateRentalRequest
            {
                VehicleId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(3)
            };
            var command = new CreateRentalCommand(request);

            var rentals = new List<Rental>
            {
                new Rental
                {
                    VehicleId = request.VehicleId,
                    StartDate = DateTime.UtcNow.AddDays(2),
                    EndDate = DateTime.UtcNow.AddDays(4),
                    IsDeleted = false
                }
            }.BuildMockDbSet();

            _mockContext.Setup(c => c.Rentals).Returns(rentals.Object);

            
            var result = await _handler.Handle(command, CancellationToken.None);

            
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Vehicle is already booked for the selected dates.");
        }

        [Fact]
        public async Task Should_Apply_Promo_Code_Discount()
        {
            
            var vehicleId = Guid.NewGuid();
            var vehicleTypeId = Guid.NewGuid();
            var request = new CreateRentalRequest
            {
                VehicleId = vehicleId,
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                PromoCode = "SAVE20"
            };
            var command = new CreateRentalCommand(request);

            var vehicles = new List<Vehicle>
            {
                new Vehicle
                {
                    Id = vehicleId,
                    VehicleTypeId = vehicleTypeId,
                    IsDeleted = false,
                    VehicleType = new VehicleType { Id = vehicleTypeId, BasePrice = 100 }
                }
            }.BuildMockDbSet();

            var promoCodes = new List<PromoCode>
            {
                new PromoCode
                {
                    Code = "SAVE20",
                    DiscountPercentage = 20,
                    IsActive = true,
                    ExpiryDate = DateTime.UtcNow.AddDays(1),
                    IsDeleted = false
                }
            }.BuildMockDbSet();

            var rentalsMock = new List<Rental>().BuildMockDbSet();
            Rental? capturedRental = null;
            rentalsMock.Setup(m => m.Add(It.IsAny<Rental>()))
                .Callback<Rental>(r => capturedRental = r);

            _mockContext.Setup(c => c.Rentals).Returns(rentalsMock.Object);
            _mockContext.Setup(c => c.Vehicles).Returns(vehicles.Object);
            _mockContext.Setup(c => c.PromoCodes).Returns(promoCodes.Object);
            _mockHolidayService.Setup(s => s.IsHolidayAsync(It.IsAny<DateTime>(), It.IsAny<string>())).ReturnsAsync(false);

            
            var result = await _handler.Handle(command, CancellationToken.None);

            
            result.IsSuccess.Should().BeTrue();
            capturedRental.Should().NotBeNull();
            capturedRental!.TotalPrice.Should().Be(80); 
        }
    }
}
