using Microsoft.AspNetCore.Mvc;
using RentARide.Application.Auth.Commands.Login;
using RentARide.Application.Auth.Commands.Register;

namespace RentARide.API.Controllers
{
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleCreatedResult(result, nameof(Login), null);
        }
    }
}
