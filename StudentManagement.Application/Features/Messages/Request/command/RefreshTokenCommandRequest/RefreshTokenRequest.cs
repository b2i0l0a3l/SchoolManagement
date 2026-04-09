using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Domain.common;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.Command.RefreshTokenCommandRequest
{
    public class RefreshTokenRequest : IRequest<Result<AuthResponse>>
    {
        public string RefreshToken { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? TokenId { get; set; } = string.Empty;

    }
}