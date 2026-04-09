using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Features.Messages.Request.command.AttendanceCommandRequest;
using StudentManagement.Application.Features.Messages.Request.Query.AttendanceQueryRequest;

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Attendance")]
    [Authorize]
    public class AttendanceController : ApiControllerBase
    {
        private IMediator _Mediator;
        public AttendanceController(IMediator mediator) => _Mediator = mediator;

        [HttpPost("AddAttendance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAttendance([FromBody] AddNewAttendanceRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpDelete("DeleteAttendance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAttendance([FromQuery] DeleteAttendanceRequest req)
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
            var result = await _Mediator.Send(new GetAllAttendancesRequest { PageNumber = pageNumber, PageSize = pageSize });
            return ReturnResult(result);
        }

        [HttpGet("GetByStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetByStudent([FromQuery] GetAttendanceByStudentRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }
    }
}
