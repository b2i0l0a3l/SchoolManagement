using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Features.Messages.Request.command.DepartmentCommandRequest;
using StudentManagement.Application.Features.Messages.Request.Query.DepartmentQueryRequest;

namespace StudentManagement.Api.Controller
{
    [ApiController]
    [Route("api/Departement")]
    [Authorize]
    public class DepartementController : ControllerBase
    {
        private readonly IMediator _Mediator;
        public DepartementController(IMediator mediator) => _Mediator = mediator;

        [HttpPost("AddNewDepartement")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> AddNewDepartement([FromBody] AddNewDepartmentRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : StatusCode(500, result);

        } 
       
        [HttpGet("GetDepartementById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetDepartementById([FromQuery]GetDepartmentByIdRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : NotFound(result);

        }
        [HttpDelete("DeleteDepartement")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteDepartement([FromQuery]DeleteDepartmentRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        [HttpPut("UpdateDepartement")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> UpdateDepartement([FromBody]UpdateDepartmentRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        
        [HttpGet("GetAllDepartements")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllDepartements()
        {
            var result = await _Mediator.Send(new GetAllDepartmentsRequest());
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}