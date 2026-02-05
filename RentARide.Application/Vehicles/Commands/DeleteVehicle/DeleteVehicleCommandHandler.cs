using MediatR;
using RentARide.Application.Common.Interfaces;
using RentARide.Application.Common.Models;

namespace RentARide.Application.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, ServiceResult<bool>>
    {
        private readonly IApplicationDbContext _context;

        public DeleteVehicleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<bool>> Handle(DeleteVehicleCommand command, CancellationToken cancellationToken)
        {
            var vehicle = await _context.Vehicles.FindAsync(new object[] { command.Id }, cancellationToken);

            if (vehicle == null)
            {
                return ServiceResult<bool>.Failure("Vehicle not found");
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult<bool>.Success(true);
        }
    }
}
