using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Features.Messages.Request.command.ParentCommandRequest;
using StudentManagement.Application.Features.Messages.Request.Query.ParentQueryRequest;

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Parent")]
    [Authorize]
    public class ParentController : ApiControllerBase
    {
        private IMediator _Mediator;
        public ParentController(IMediator mediator) => _Mediator = mediator;

        [HttpPost("AddParent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddParent([FromBody] AddNewParentRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpDelete("DeleteParent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteParent([FromQuery] DeleteParentRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Mediator.Send(new GetAllParentsRequest());
            return ReturnResult(result);
        }

        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromQuery] GetParentByIdRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }
    }
}
