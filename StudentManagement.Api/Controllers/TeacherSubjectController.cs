using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Features.Messages.Request.command.TeacherSubjectCommandRequest;
using StudentManagement.Application.Features.Messages.Request.Query.TeacherSubjectQueryRequest;
using StudentManagement.Domain.Common;

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/TeacherSubject")]
    [Authorize]
    public class TeacherSubjectController : ApiControllerBase
    {
        private readonly IMediator _Mediator;
        public TeacherSubjectController(IMediator mediator) => _Mediator = mediator;

        [HttpPost("AddNewTeacherSubject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles="Admin")]

        public async Task<IActionResult> AddNewTeacherSubject([FromBody] AddNewTeacherSubjectRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpGet("GetTeacherSubjectById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetTeacherSubjectById([FromQuery] GetTeacherSubjectByIdRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpDelete("DeleteTeacherSubject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteTeacherSubject([FromQuery] DeleteTeacherSubjectRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpPut("UpdateTeacherSubject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> UpdateTeacherSubject([FromBody] UpdateTeacherSubjectRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpGet("GetAllTeacherSubjects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllTeacherSubjects()
        {
            var result = await _Mediator.Send(new GetAllTeacherSubjectsRequest());
            return ReturnResult(result);
        }
    }
}