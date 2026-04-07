using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Features.Messages.Request.command.GradeCommandRequest;
using StudentManagement.Application.Features.Messages.Request.Query.GradeQueryRequest;
using StudentManagement.Domain.Common;

namespace StudentManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Grade")]
    public class GradeController : ApiControllerBase
    {
        private readonly IMediator _Mediator;
        public GradeController(IMediator mediator) => _Mediator = mediator;

        [HttpPost("AddNewGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles ="Teacher")]
        public async Task<IActionResult> AddNewGrade([FromBody] AddNewGradeRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpGet("GetGradeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles ="Admin,Teacher")]
        public async Task<IActionResult> GetGradeById([FromQuery] GetGradeByIdRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpDelete("DeleteGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteGrade([FromQuery] DeleteGradeRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpPut("UpdateGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles ="Admin,Teacher")]
        public async Task<IActionResult> UpdateGrade([FromBody] UpdateGradeRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpGet("GetAllGrades")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetAllGrades()
        {
            var result = await _Mediator.Send(new GetAllGradesRequest());
            return ReturnResult(result);
        }
    }
}