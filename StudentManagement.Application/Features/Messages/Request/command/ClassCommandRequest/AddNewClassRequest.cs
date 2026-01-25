using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.command.ClassCommandRequest
{
    public class AddNewClassRequest  : IRequest<Result<ClassModel>>
    {
        public string ClassName { get; set; } = string.Empty;
        public DateOnly Year { get; set; } 

    }
}