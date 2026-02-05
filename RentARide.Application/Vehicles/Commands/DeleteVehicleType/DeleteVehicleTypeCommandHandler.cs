using MediatR;
using Microsoft.Extensions.Caching.Memory;
using RentARide.Application.Common.Interfaces;
using RentARide.Application.Common.Models;

namespace RentARide.Application.Vehicles.Commands.DeleteVehicleType
{
    public class DeleteVehicleTypeCommandHandler : IRequestHandler<DeleteVehicleTypeCommand, ServiceResult<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public DeleteVehicleTypeCommandHandler(IApplicationDbContext context, IMemoryCache _cache)
        {
            _context = context;
            this._cache = _cache;
        }

        public async Task<ServiceResult<bool>> Handle(DeleteVehicleTypeCommand command, CancellationToken cancellationToken)
        {
            var vehicleType = await _context.VehicleTypes.FindAsync(new object[] { command.Id }, cancellationToken);

            if (vehicleType == null)
            {
                return ServiceResult<bool>.Failure("Vehicle Type not found");
            }

            _context.VehicleTypes.Remove(vehicleType);
            await _context.SaveChangesAsync(cancellationToken);

            
            _cache.Remove("VehicleTypes");

            return ServiceResult<bool>.Success(true);
        }
    }
}
