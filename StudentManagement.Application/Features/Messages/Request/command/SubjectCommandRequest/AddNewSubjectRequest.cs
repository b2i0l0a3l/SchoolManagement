using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.command.SubjectCommandRequest
{
    public class AddNewSubjectRequest : IRequest<Result<SubjectModel>>
    {
        public string SubjectName { get; set; } = string.Empty;
        public DateTime CreatAt { get; set; } = DateTime.UtcNow;
    }
}
