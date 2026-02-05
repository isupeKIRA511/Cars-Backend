using MediatR;
using RentARide.Application.Common.Models;
using RentARide.Domain.Enums;

namespace RentARide.Application.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<ServiceResult<Guid>>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.Customer; 
    }
}
