using MediatR;
using RentARide.Application.Common.Interfaces;
using RentARide.Application.Common.Models;
using RentARide.Domain.Entities;

namespace RentARide.Application.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, ServiceResult<Guid>>
    {
        private readonly IApplicationDbContext _context;

        public CreateVehicleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<Guid>> Handle(CreateVehicleCommand command, CancellationToken cancellationToken)
        {
            var vehicle = new Vehicle
            {
                Make = command.Make,
                Model = command.Model,
                Year = command.Year,
                LicensePlate = command.LicensePlate,
                VehicleTypeId = command.VehicleTypeId
            };

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.Success(vehicle.Id);
        }
    }
}
