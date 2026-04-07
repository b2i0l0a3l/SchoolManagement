using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Features.Messages.Request.command.DepartmentCommandRequest;
using StudentManagement.Application.Features.Messages.Request.Query.DepartmentQueryRequest;

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Department")]
    [Authorize]
    public class DepartmentController : ApiControllerBase
    {
        private readonly IMediator _Mediator;
        public DepartmentController(IMediator mediator) => _Mediator = mediator;

        [HttpPost("AddNewDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> AddNewDepartment([FromBody] AddNewDepartmentRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);

        } 
       
        [HttpGet("GetDepartmentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetDepartmentById([FromQuery]GetDepartmentByIdRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);

        }
        [HttpDelete("DeleteDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteDepartment([FromQuery]DeleteDepartmentRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }
        [HttpPut("UpdateDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> UpdateDepartment([FromBody]UpdateDepartmentRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }
        
        [HttpGet("GetAllDepartments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllDepartments()
        {
            var result = await _Mediator.Send(new GetAllDepartmentsRequest());
            return ReturnResult(result);
        }
    }
}