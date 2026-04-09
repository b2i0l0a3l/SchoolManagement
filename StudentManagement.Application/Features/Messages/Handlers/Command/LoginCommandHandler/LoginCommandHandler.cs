using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StoreSystem.Core.Entities;
using StudentManagement.Application.Features.Messages.Request.command.LoginRequest;
using StudentManagement.Application.Util;
using StudentManagement.Application.Util.jwt;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.LoginCommandHandler
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, Result<AuthResponse>>
    {
        private readonly UserManager<User> _Manager;
        private readonly IRepository<RefreshToken> _Repo;
        private readonly IGenerateJwtToken _JwtService;
        private readonly ITokenService _TokenService;
        public LoginCommandHandler(IRepository<RefreshToken> repo, ITokenService TokenService, UserManager<User> manager, IGenerateJwtToken jwtService)
        {
            _Manager = manager;
            _JwtService = jwtService;
            _TokenService = TokenService;
            _Repo = repo;
        }
        public async Task<Result<AuthResponse>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            User? existing = await _Manager.FindByEmailAsync(request.Email);
            if (existing == null) return Errors.UserNotFoundError;

            if (!await _Manager.CheckPasswordAsync(existing, request.Password))
                return new Error("InvalidCredentialsError", ErrorType.Validation, "Invalid credentials");
            string TokenId = _TokenService.Generate(16);
            Claim[] claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, existing.Id.ToString()),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Role, existing.Role.ToString()),
                new Claim("TokenId", TokenId)
            };

            var newRefreshToken = _TokenService.Generate(64);
            RefreshToken refreshToken = new()
            {
                UserId = existing.Id,
                TokenId = TokenId,
                RefreshTokenHash = BCrypt.Net.BCrypt.HashPassword(newRefreshToken),
                RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(30),
                RefreshTokenRevokedAt = null
            };
            await _Repo.Add(refreshToken);

            Result<string> AccessToken = _JwtService.GenerateJwtToken(claims);
            if (!AccessToken.IsSuccess || string.IsNullOrEmpty(AccessToken.Value)) return new Error("InternalServerError", ErrorType.General, "Error Happend By Generating Jwt Token.");
            return new AuthResponse() { Token = AccessToken.Value, RefreshToken = newRefreshToken, TokenId = TokenId };
        }
    }

}