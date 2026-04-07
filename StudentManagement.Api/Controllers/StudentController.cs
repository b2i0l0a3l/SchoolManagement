using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Features.Messages.Request.command.StudentCommandRequest;
using StudentManagement.Application.Features.Messages.Request.Query.StudentQueryRequest;

namespace StudentManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Student")]
    [Authorize(Roles = "Admin")]
    public class StudentController : ApiControllerBase
    {
        private readonly IMediator _Mediator;
        public StudentController(IMediator mediator) => _Mediator = mediator;

        
        [HttpPost("AddNewStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewStudent([FromBody] AddNewStudentRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpGet("GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetStudentById([FromQuery] GetStudentByIdRequest req)
        {
 
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpDelete("DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStudent([FromQuery] DeleteStudentRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpPut("UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpGet("GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _Mediator.Send(new GetAllStudentsRequest());
            return ReturnResult(result);
        }

       
        

    }
}