using MediatR;
using RentARide.Application.Common.Models;
using RentARide.Application.DTOs.Rentals;

namespace RentARide.Application.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommand : IRequest<ServiceResult<Guid>>
    {
         public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public Guid VehicleTypeId { get; set; }
    }
}
