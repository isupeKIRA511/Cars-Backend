using MediatR;
using RentARide.Application.Common.Interfaces;
using RentARide.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace RentARide.Application.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, ServiceResult<bool>>
    {
        private readonly IApplicationDbContext _context;

        public UpdateVehicleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<bool>> Handle(UpdateVehicleCommand command, CancellationToken cancellationToken)
        {
            var vehicle = await _context.Vehicles.FindAsync(new object[] { command.Id }, cancellationToken);

            if (vehicle == null)
            {
                return ServiceResult<bool>.Failure("Vehicle not found");
            }

            vehicle.Make = command.Make;
            vehicle.Model = command.Model;
            vehicle.Year = command.Year;
            vehicle.LicensePlate = command.LicensePlate;
            vehicle.VehicleTypeId = command.VehicleTypeId;

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult<bool>.Success(true);
        }
    }
}
