using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Features.Messages.Request.command.LoginRequest;
using StudentManagement.Application.Features.Messages.Request.command.Register;

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ApiControllerBase
    {
        private readonly IMediator _Mediator;
        public AuthController(IMediator mediator)
        {
            _Mediator = mediator;
        }
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            var result = await _Mediator.Send(req);
            return ReturnResult(result);
        }
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginCommandRequest req)
        {
            var result = await _Mediator.Send(req);
            if (!result.IsSuccess)
            {
                return Unauthorized(result.Error);
            }
            return Ok(result.Value);
        }
    }
}