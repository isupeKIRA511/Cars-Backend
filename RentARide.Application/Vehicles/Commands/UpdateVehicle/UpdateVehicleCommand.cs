using MediatR;
using RentARide.Application.Common.Models;

namespace RentARide.Application.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommand : IRequest<ServiceResult<bool>>
    {
        public Guid Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public Guid VehicleTypeId { get; set; }
    }
}
