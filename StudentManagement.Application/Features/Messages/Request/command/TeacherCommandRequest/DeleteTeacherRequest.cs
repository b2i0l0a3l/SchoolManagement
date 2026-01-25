using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.command.TeacherCommandRequest
{
    public class DeleteTeacherRequest : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }
}
