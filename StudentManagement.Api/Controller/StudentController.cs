using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Features.Messages.Handlers.Query.StudentQueryHandler;
using StudentManagement.Application.Features.Messages.Request.command.StudentCommandRequest;
using StudentManagement.Application.Features.Messages.Request.Query.Student;
using StudentManagement.Application.Features.Messages.Request.Query.StudentQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Api.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/Student")]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _Mediator;
        public StudentController(IMediator mediator) => _Mediator = mediator;

        
        [HttpPost("AddNewStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> AddNewStudent([FromBody] AddNewStudentRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : StatusCode(500, result);
        }

        [HttpGet("GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Student,Admin")]
        public async Task<IActionResult> GetStudentById([FromQuery] GetStudentByIdRequest req)
        {
            if (GetUserRole() != "Admin" && GetUserID() != req.StudentId.ToString())
                return Forbid();
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpDelete("DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent([FromQuery] DeleteStudentRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPut("UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpGet("GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles ="Admin,Teacher")]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _Mediator.Send(new GetAllStudentsRequest());
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpGet("GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetPassedStudents()
        {
            var result = await _Mediator.Send(new GetPassedStudent());
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        [HttpGet("GetPassedStudentsByClass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetPassedStudentsByClass([FromQuery] GetPassedStudentByClassIdQueryRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        
        private string? GetUserID()
        => User.FindFirstValue("StudentId");
        private string? GetUserRole()
        => User.FindFirstValue(ClaimTypes.Role);


    }
}