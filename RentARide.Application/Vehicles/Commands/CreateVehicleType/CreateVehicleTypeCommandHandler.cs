using MediatR;
using Microsoft.Extensions.Caching.Memory;
using RentARide.Application.Common.Interfaces;
using RentARide.Application.Common.Models;
using RentARide.Domain.Entities;

namespace RentARide.Application.Vehicles.Commands.CreateVehicleType
{
    public class CreateVehicleTypeCommandHandler : IRequestHandler<CreateVehicleTypeCommand, ServiceResult<Guid>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public CreateVehicleTypeCommandHandler(IApplicationDbContext context, IMemoryCache _cache)
        {
            _context = context;
            this._cache = _cache;
        }

        public async Task<ServiceResult<Guid>> Handle(CreateVehicleTypeCommand command, CancellationToken cancellationToken)
        {
            var vehicleType = new VehicleType
            {
                Name = command.Name,
                Description = command.Description
            };

            _context.VehicleTypes.Add(vehicleType);
            await _context.SaveChangesAsync(cancellationToken);

            
            _cache.Remove("VehicleTypes");

            return ServiceResult<Guid>.Success(vehicleType.Id);
        }
    }
}
