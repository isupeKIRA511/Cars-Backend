using MediatR;
using RentARide.Application.Common.Models;
using RentARide.Application.DTOs.Rentals;

namespace RentARide.Application.Rentals.Commands.CreateRental
{
    public class CreateRentalCommand : IRequest<ServiceResult<Guid>>
    {
        public CreateRentalRequest Request { get; set; }

        public CreateRentalCommand(CreateRentalRequest request)
        {
            Request = request;
        }
    }
}
