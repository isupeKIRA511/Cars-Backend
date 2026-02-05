using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RentARide.Application.Common.Interfaces;
using RentARide.Application.Common.Models;
using RentARide.Application.DTOs.Vehicles;
using Mapster;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RentARide.Application.Vehicles.Queries.GetVehicleTypes
{
    public class GetVehicleTypesQueryHandler : IRequestHandler<GetVehicleTypesQuery, ServiceResult<List<VehicleTypeDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public GetVehicleTypesQueryHandler(IApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<ServiceResult<List<VehicleTypeDto>>> Handle(GetVehicleTypesQuery request, CancellationToken cancellationToken)
        {
            const string cacheKey = "VehicleTypes";

            if (!_cache.TryGetValue(cacheKey, out List<VehicleTypeDto>? vehicleTypes))
            {
                
                vehicleTypes = await _context.VehicleTypes
                    .AsNoTracking()
                    .ProjectToType<VehicleTypeDto>()
                    .ToListAsync(cancellationToken);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30));

                _cache.Set(cacheKey, vehicleTypes, cacheEntryOptions);
            }

            return ServiceResult<List<VehicleTypeDto>>.Success(vehicleTypes!);
        }
    }
}
