using MediatR;
using Microsoft.EntityFrameworkCore;
using RentARide.Application.Common.Interfaces;
using RentARide.Application.Common.Models;

namespace RentARide.Application.Rentals.Commands.UpdateRentalStatus
{
    public class UpdateRentalStatusCommandHandler : IRequestHandler<UpdateRentalStatusCommand, ServiceResult<bool>>
    {
        private readonly IApplicationDbContext _context;

        public UpdateRentalStatusCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<bool>> Handle(UpdateRentalStatusCommand request, CancellationToken cancellationToken)
        {
            var rental = await _context.Rentals.FindAsync(new object[] { request.Id }, cancellationToken);

            if (rental == null)
            {
                return ServiceResult<bool>.Failure("Rental not found");
            }

            rental.Status = request.Status;
            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult<bool>.Success(true);
        }
    }
}
