using Microsoft.AspNetCore.Mvc;
using StudentManagement.Domain.Common;

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult ReturnResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return HandleError(result.Error);
        }

        protected IActionResult ReturnResult(Result result)
        {
            if (result.IsSuccess)
            {
                return Ok();
            }

            return HandleError(result.Error);
        }

        private IActionResult HandleError(Error? error)
        {
            if (error == null)
                return StatusCode(500, new { title = "Unknown error occurred" });

            return error.Type switch
            {
                ErrorType.NotFound => NotFound(error),
                ErrorType.Validation => BadRequest(error),
                _ => BadRequest(error)
            };
        }
    }
}
