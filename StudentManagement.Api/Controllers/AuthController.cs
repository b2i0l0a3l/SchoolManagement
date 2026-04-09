using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using StudentManagement.Application.Features.Messages.Request.command.LoginRequest;
using StudentManagement.Application.Features.Messages.Request.command.Register;
using StudentManagement.Application.Features.Messages.Request.Command.Logout;
using StudentManagement.Application.Features.Messages.Request.Command.RefreshTokenCommandRequest;

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
        [EnableRateLimiting("AuthLimiter")]
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
        [HttpPost("Refresh")]
        [EnableRateLimiting("AuthLimiter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var result = await _Mediator.Send(request);
            return ReturnResult(result);
        }

        [HttpPost("Logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            var result = await _Mediator.Send(request);
            return ReturnResult(result);
        }
    }
}