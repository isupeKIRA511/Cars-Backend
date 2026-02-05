using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentARide.Application.Common.Models;

namespace RentARide.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private ISender? _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        protected ActionResult HandleResult<T>(ServiceResult<T> result)
        {
            if (result == null) return NotFound(ApiResponse<T>.Failure("Resource not found"));

            if (result.IsSuccess)
            {
                if (result.Data == null)
                    return Ok(ApiResponse<T>.Success(default!, "Operation successful"));
                
                return Ok(ApiResponse<T>.Success(result.Data, "Operation successful"));
            }

            if (!result.IsSuccess)
            {
                if (result.Error == "Not Found" || (result.Error?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true))
                    return NotFound(ApiResponse<T>.Failure(result.Error ?? "Resource not found"));

                if (result.ValidationErrors != null && result.ValidationErrors.Any())
                    return BadRequest(ApiResponse<T>.Failure(result.ValidationErrors, "Validation failed"));

                return BadRequest(ApiResponse<T>.Failure(result.Error ?? "Bad request"));
            }

            return BadRequest(ApiResponse<T>.Failure("An unknown error occurred"));
        }

        protected ActionResult HandleCreatedResult<T>(ServiceResult<T> result, string actionName, object? routeValues)
        {
            if (result.IsSuccess)
            {
                return CreatedAtAction(actionName, routeValues, ApiResponse<T>.Success(result.Data!, "Resource created successfully"));
            }
            return HandleResult(result);
        }
    }
}
