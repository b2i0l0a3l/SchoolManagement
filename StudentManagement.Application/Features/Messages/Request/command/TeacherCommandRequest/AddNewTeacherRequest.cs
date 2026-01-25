using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.command.TeacherCommandRequest
{
    public class AddNewTeacherRequest : IRequest<Result<TeacherModel>>
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime HireDate { get; set; } = DateTime.UtcNow;
        public int DepartmentId { get; set; }
    }
}
