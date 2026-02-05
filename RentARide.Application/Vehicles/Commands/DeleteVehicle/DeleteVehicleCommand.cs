using MediatR;
using RentARide.Application.Common.Models;

namespace RentARide.Application.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommand : IRequest<ServiceResult<bool>>
    {
        public Guid Id { get; set; }

        public DeleteVehicleCommand(Guid id)
        {
            Id = id;
        }
    }
}
