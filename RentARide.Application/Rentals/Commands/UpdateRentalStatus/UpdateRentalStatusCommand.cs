using MediatR;
using RentARide.Application.Common.Models;
using RentARide.Domain.Enums;

namespace RentARide.Application.Rentals.Commands.UpdateRentalStatus
{
    public class UpdateRentalStatusCommand : IRequest<ServiceResult<bool>>
    {
        public Guid Id { get; set; }
        public RentalStatus Status { get; set; }

        public UpdateRentalStatusCommand(Guid id, RentalStatus status)
        {
            Id = id;
            Status = status;
        }
    }
}
