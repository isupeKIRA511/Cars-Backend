using MediatR;
using Microsoft.EntityFrameworkCore;
using RentARide.Application.Common.Interfaces;
using RentARide.Application.Common.Models;
using RentARide.Domain.Entities;
using RentARide.Domain.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RentARide.Application.Rentals.Commands.CreateRental
{
    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, ServiceResult<Guid>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHolidayService _holidayService;

        public CreateRentalCommandHandler(IApplicationDbContext context, IHolidayService holidayService)
        {
            _context = context;
            _holidayService = holidayService;
        }

        public async Task<ServiceResult<Guid>> Handle(CreateRentalCommand command, CancellationToken cancellationToken)
        {
            var req = command.Request;

            
            bool isVehicleBooked = await _context.Rentals
                .AnyAsync(r => r.VehicleId == req.VehicleId &&
                               r.EndDate >= req.StartDate &&
                               r.StartDate <= req.EndDate &&
                               !r.IsDeleted, cancellationToken);

            if (isVehicleBooked)
            {
                return ServiceResult<Guid>.Failure("Vehicle is already booked for the selected dates.");
            }

            
            var vehicle = await _context.Vehicles
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(v => v.Id == req.VehicleId, cancellationToken);

            if (vehicle == null)
            {
                return ServiceResult<Guid>.Failure("Vehicle not found.");
            }

            
            decimal basePrice = vehicle.VehicleType?.BasePrice ?? 0;
            
            
            decimal amenitiesPrice = 0;
            if (req.AmenityIds != null && req.AmenityIds.Any())
            {
                 amenitiesPrice = await _context.Amenities
                    .Where(a => req.AmenityIds.Contains(a.Id))
                    .SumAsync(a => a.Price, cancellationToken);
            }

            
            var totalDays = (req.EndDate - req.StartDate).Days;
            if (totalDays <= 0) totalDays = 1; 

            
            bool isHoliday = await _holidayService.IsHolidayAsync(req.StartDate);
            
            decimal dailyRate = basePrice;
            if (isHoliday)
            {
                dailyRate += basePrice * 0.10m; 
            }

            decimal totalPrice = (dailyRate * totalDays) + amenitiesPrice;

            
            if (!string.IsNullOrWhiteSpace(req.PromoCode))
            {
                var promo = await _context.PromoCodes
                    .FirstOrDefaultAsync(p => p.Code == req.PromoCode && p.IsActive && p.ExpiryDate >= DateTime.UtcNow, cancellationToken);

                if (promo != null)
                {
                    totalPrice -= totalPrice * (promo.DiscountPercentage / 100);
                }
            }

            
            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                UserId = req.UserId,
                VehicleId = req.VehicleId,
                StartDate = req.StartDate,
                EndDate = req.EndDate,
                TotalPrice = totalPrice,
                Status = RentalStatus.Pending
            };

            
            if (req.AmenityIds != null)
            {
                foreach (var amenityId in req.AmenityIds)
                {
                    rental.RentalAmenities.Add(new RentalAmenity
                    {
                        RentalId = rental.Id,
                        AmenityId = amenityId
                    });
                }
            }

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.Success(rental.Id);
        }
    }
}
