using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Features.Messages.Request.command.TeacherCommandRequest;
using StudentManagement.Application.Features.Messages.Request.Query.TeacherQueryRequest;

namespace StudentManagement.Api.Controller
{
    [ApiController]
    [Route("api/Teacher")]
    [Authorize]
    public class TeacherController : ControllerBase
    {
        private readonly IMediator _Mediator;
        public TeacherController(IMediator mediator) => _Mediator = mediator;

        [HttpPost("AddNewTeacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> AddNewTeacher([FromBody] AddNewTeacherRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : StatusCode(500, result);
        }

        [HttpGet("GetTeacherById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles="Admin,Teacher")]
        public async Task<IActionResult> GetTeacherById([FromQuery] GetTeacherByIdRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpDelete("DeleteTeacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteTeacher([FromQuery] DeleteTeacherRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPut("UpdateTeacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> UpdateTeacher([FromBody] UpdateTeacherRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : NotFound(result.Error!.Description);
        }

        [HttpGet("GetAllTeachers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var result = await _Mediator.Send(new GetAllTeachersRequest());
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}