using MediatR;
using RentARide.Application.Common.Models;
using RentARide.Application.DTOs.Vehicles;

namespace RentARide.Application.Vehicles.Queries.GetVehicleTypes
{
    public class GetVehicleTypesQuery : IRequest<ServiceResult<List<VehicleTypeDto>>>
    {
    }
}
