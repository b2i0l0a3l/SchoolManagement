using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Features.Messages.Request.command.SubjectCommandRequest;
using StudentManagement.Application.Features.Messages.Request.Query.SubjectQueryRequest;

namespace StudentManagement.Api.Controller
{
    [ApiController]
    [Route("api/Subject")]
    [Authorize]
    public class SubjectController : ControllerBase
    {
        private readonly IMediator _Mediator;
        public SubjectController(IMediator mediator) => _Mediator = mediator;

        [HttpPost("AddNewSubject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddNewSubject([FromBody] AddNewSubjectRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : StatusCode(500, result);

        } 
       
        [HttpGet("GetSubjectById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetSubjectById([FromQuery]GetSubjectByIdRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : NotFound(result);

        }
        [HttpDelete("DeleteSubject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteSubject([FromQuery]DeleteSubjectRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        [HttpPut("UpdateSubject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateSubject([FromBody]UpdateSubjectRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        
        [HttpGet("GetAllSubjects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllSubjects()
        {
            var result = await _Mediator.Send(new GetAllSubjectsRequest());
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

    }
}