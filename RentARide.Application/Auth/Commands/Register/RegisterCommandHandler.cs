using MediatR;
using RentARide.Application.Common.Interfaces;
using RentARide.Application.Common.Models;
using RentARide.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace RentARide.Application.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ServiceResult<Guid>>
    {
        private readonly IApplicationDbContext _context;

        public RegisterCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<Guid>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            
            if (string.IsNullOrWhiteSpace(command.Email) || string.IsNullOrWhiteSpace(command.Password))
                return ServiceResult<Guid>.Failure("Invalid details");

            
            if (await _context.Users.AnyAsync(u => u.Email == command.Email, cancellationToken))
                return ServiceResult<Guid>.Failure("Email already exists");

            var user = new User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Role = command.Role,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.Success(user.Id);
        }
    }
}
