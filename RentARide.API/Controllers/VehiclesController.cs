using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentARide.Application.Vehicles.Commands.CreateVehicle;
using RentARide.Application.Vehicles.Commands.UpdateVehicle;
using RentARide.Application.Vehicles.Commands.DeleteVehicle;
using RentARide.Application.Vehicles.Commands.CreateVehicleType;
using RentARide.Application.Vehicles.Commands.DeleteVehicleType;
using RentARide.Application.Vehicles.Queries.GetVehicleTypes;

namespace RentARide.API.Controllers
{
    public class VehiclesController : BaseController
    {
        [HttpGet("types")]
        public async Task<IActionResult> GetVehicleTypes()
        {
            var result = await Mediator.Send(new GetVehicleTypesQuery());
            return HandleResult(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateVehicle(CreateVehicleCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateVehicle(Guid id, UpdateVehicleCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVehicle(Guid id)
        {
            var result = await Mediator.Send(new DeleteVehicleCommand(id));
            return HandleResult(result);
        }

        [HttpPost("types")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateVehicleType(CreateVehicleTypeCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

        [HttpDelete("types/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVehicleType(Guid id)
        {
            var result = await Mediator.Send(new DeleteVehicleTypeCommand(id));
            return HandleResult(result);
        }
    }
}
