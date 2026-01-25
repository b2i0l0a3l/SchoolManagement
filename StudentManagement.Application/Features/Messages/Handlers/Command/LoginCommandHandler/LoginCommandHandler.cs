using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StudentManagement.Application.Features.Messages.Request.command.LoginRequest;
using StudentManagement.Application.Util;
using StudentManagement.Application.Util.jwt;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.LoginCommandHandler
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, Result<LoginModel>>
    {
        private readonly UserManager<User> _Manager;
        private readonly IUserRole _u;
        public LoginCommandHandler(IUserRole u,UserManager<User> manager)
        {
            _Manager = manager;
            _u = u;
        }
        public async Task<Result<LoginModel>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            User? existing = await _Manager.FindByEmailAsync(request.Email);
            if (existing == null) return Errors.UserNotFoundError;

            if (!await _Manager.CheckPasswordAsync(existing, request.Password))
                return new Error("InvalidCredentialsError", ErrorType.Validation, "Invalid credentials");

            Result<string> AccessToken = await _u.AddRoleToUserAndGenerateJwtKey(existing, existing.Role ?? Roles.User);
            if (!AccessToken.IsSuccess || string.IsNullOrEmpty(AccessToken.Value)) return new Error("InternalServerError", ErrorType.General, "Error Happend By Generating Jwt Token.");
            return new LoginModel() { AccessKey = AccessToken.Value };
        }
    }
}