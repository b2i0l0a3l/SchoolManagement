using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.command.TeacherCommandRequest
{
    public class UpdateTeacherRequest : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime HireDate { get; set; } = DateTime.Now;
        public int DepartmentId { get; set; }
    }
}
