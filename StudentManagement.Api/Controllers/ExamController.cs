using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Features.Messages.Request.command.ExamCommandRequest;
using StudentManagement.Application.Features.Messages.Request.Query.ExamQueryRequest;

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Exam")]
    [Authorize]
    public class ExamController : ApiControllerBase
    {
        private IMediator _Mediator;
        public ExamController(IMediator mediator) => _Mediator = mediator;

        [HttpPost("AddExam")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddExam([FromBody] AddNewExamRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpPut("UpdateExam")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateExam([FromBody] UpdateExamRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpDelete("DeleteExam")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteExam([FromQuery] DeleteExamRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _Mediator.Send(new GetAllExamsRequest { PageNumber = pageNumber, PageSize = pageSize });
            return ReturnResult(result);
        }

        [HttpGet("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromQuery] GetExamByIdRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }
    }
}
