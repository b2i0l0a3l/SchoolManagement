using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Features.Messages.Request.command.LoginRequest;
using StudentManagement.Application.Features.Messages.Request.command.Register;

namespace StudentManagement.Api.Controller
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _Mediator;
        public AuthController(IMediator mediator)
        {
            _Mediator = mediator;
        }
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : StatusCode(500, result);
        }
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginCommandRequest req)
        {
            var result = await _Mediator.Send(req);
            return result.IsSuccess ? Ok(result) : Unauthorized(result);
        }
    }
}