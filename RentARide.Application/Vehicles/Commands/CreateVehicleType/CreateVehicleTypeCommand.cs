using MediatR;
using RentARide.Application.Common.Models;

namespace RentARide.Application.Vehicles.Commands.CreateVehicleType
{
    public class CreateVehicleTypeCommand : IRequest<ServiceResult<Guid>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
