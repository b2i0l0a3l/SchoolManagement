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

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Class")]
    [Authorize]
    public class ClassController : ApiControllerBase
    {
        private IMediator _Mediator;
        public ClassController(IMediator mediator) => _Mediator = mediator;

        [HttpPost("AddNewClass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> AddNewClass([FromBody] AddNewClassRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }
        [HttpGet("GetClassById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetClassById([FromQuery]GetClassByIdRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }
        [HttpDelete("DeleteClass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteClass([FromQuery]DeleteClassRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }
        [HttpPut("UpdateClass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize("Admin")]
        public async Task<IActionResult> UpdateClass([FromBody]UpdateClassRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }

        [HttpGet("GetAllClasses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllClasses([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _Mediator.Send(new GetAllClassesRequest { PageNumber = pageNumber, PageSize = pageSize });
            return ReturnResult(result);
        }
        [HttpGet("GetClassStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetClassStudent([FromQuery] GetClassStudentRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }
    }
}