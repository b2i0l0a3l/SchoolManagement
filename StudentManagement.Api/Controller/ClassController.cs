using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Features.Messages.Request.command.ClassCommandRequest;
using StudentManagement.Application.Features.Messages.Request.Query.ClassQueryRequest;

namespace StudentManagement.Api.Controller
{
    [ApiController]
    [Route("api/Class")]
    [Authorize]
    public class ClassController : ControllerBase
    {
        private IMediator _Mediator;
        public ClassController(IMediator mediator) => _Mediator = mediator;

        [HttpPost("AddNewClass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> AddNewClass([FromBody] AddNewClassRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : StatusCode(500, result);
        }
        [HttpGet("GetClassById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetClassById([FromQuery]GetClassByIdRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        [HttpDelete("DeleteClass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteClass([FromQuery]DeleteClassRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        [HttpPut("UpdateClass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize("Admin")]
        public async Task<IActionResult> UpdateClass([FromBody]UpdateClassRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpGet("GetAllClasses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllClasses()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _Mediator.Send(new GetAllClassesRequest());
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        [HttpGet("GetClassStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetClassStudent([FromQuery] GetClassStudentRequest req)
        {
           if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}