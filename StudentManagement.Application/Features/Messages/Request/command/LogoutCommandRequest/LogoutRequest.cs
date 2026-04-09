using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Domain.Common;

namespace StudentManagement.Application.Features.Messages.Request.Command.Logout
{
    public class LogoutRequest : IRequest<Result>
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string? TokenId { get; set; } 
    }
}