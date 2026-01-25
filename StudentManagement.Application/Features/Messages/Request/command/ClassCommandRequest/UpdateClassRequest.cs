using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.command.ClassCommandRequest
{
    public class UpdateClassRequest  : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public DateOnly Year { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    }
}