using MediatR;
using RentARide.Application.Common.Models;

namespace RentARide.Application.Vehicles.Commands.DeleteVehicleType
{
    public class DeleteVehicleTypeCommand : IRequest<ServiceResult<bool>>
    {
        public Guid Id { get; set; }

        public DeleteVehicleTypeCommand(Guid id)
        {
            Id = id;
        }
    }
}
