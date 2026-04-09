using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StoreSystem.Core.Entities;
using StudentManagement.Domain.Models;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Application.Util.jwt;
using StudentManagement.Application.Features.Messages.Request.Command.RefreshTokenCommandRequest;

namespace StoreSystem.Application.Feature.Messages.handler.Command.Refresh
{
    public class RefreshHandler : IRequestHandler<RefreshTokenRequest, Result<AuthResponse>>
    {
        private readonly ITokenService _GenerateRefreshToken;
        private readonly IGenerateJwtToken _GenerateJwtToken;
        private readonly UserManager<User> _UManager;
        private readonly IRepository<RefreshToken> _Repo;
        public RefreshHandler(IRepository<RefreshToken> rep,IGenerateJwtToken GenerateJwtToken,UserManager<User> UManager,ITokenService GenerateRefreshToken)
        {
            _GenerateRefreshToken = GenerateRefreshToken;
            _UManager = UManager;
            _GenerateJwtToken = GenerateJwtToken;
            _Repo = rep;
        } 
        public async Task<Result<AuthResponse>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            User? user = await _UManager.FindByEmailAsync(request.Email);
            if (user == null) return Errors.UserNotFoundError;

            if (string.IsNullOrEmpty(request.TokenId)) return new Error("RefreshTokenError", ErrorType.NotFound, "Token Id is Required!");

            Result<RefreshToken?> result  = await _Repo.GetByCondition(x=>x.TokenId == request.TokenId);

            if (result.Value == null) return new Error("RefreshTokenError", ErrorType.NotFound, "Refresh Token Not found!");

            RefreshToken refreshToken = result.Value;
         

            if(refreshToken == null) 
                return new Error("RefreshTokenERROR", ErrorType.Validation, "Invalid refresh token");
            if (refreshToken.RefreshTokenRevokedAt != null)
                return new Error("RefreshTokenRevokedError", ErrorType.Validation, "Refresh token is revoked");
            if (refreshToken.RefreshTokenExpiresAt == null || refreshToken.RefreshTokenExpiresAt <= DateTime.UtcNow)
                return new Error("RefreshTokenExpiredError", ErrorType.Validation, "Refresh token expired");
            bool isVerified = BCrypt.Net.BCrypt.Verify(request.RefreshToken, refreshToken.RefreshTokenHash);
            if(!isVerified)
                return new Error("RefreshTokenERROR", ErrorType.Validation, "Invalid refresh token");
 
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("TokenId", refreshToken.TokenId),
            };
           
            string newAccessToken = _GenerateJwtToken.GenerateJwtToken(claims);
            string newRefreshToken = _GenerateRefreshToken.Generate(64);

            await _Repo.Update(refreshToken.Id, x => 
            {
                x.UserId = user.Id;
                x.RefreshTokenHash = BCrypt.Net.BCrypt.HashPassword(newRefreshToken);
                x.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);
            });

            AuthResponse model = new()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };
            return model;
    }
    }
}