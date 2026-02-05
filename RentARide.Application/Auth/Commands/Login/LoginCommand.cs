using MediatR;
using RentARide.Application.Common.Models;

namespace RentARide.Application.Auth.Commands.Login
{
    public class LoginCommand : IRequest<ServiceResult<string>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
