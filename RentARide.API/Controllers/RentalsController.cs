using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentARide.Application.DTOs.Rentals;
using RentARide.Application.Rentals.Commands.CreateRental;

namespace RentARide.API.Controllers
{
    [Authorize]
    public class RentalsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateRental(CreateRentalRequest request)
        {
            var command = new CreateRentalCommand(request);
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }
    }
}
