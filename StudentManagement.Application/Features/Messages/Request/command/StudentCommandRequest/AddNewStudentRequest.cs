using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.enums;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.command.StudentCommandRequest
{
    public class AddNewStudentRequest : IRequest<Result<StudentModel>>
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        public enGender Gender { get; set; } 
        public int ClassId { get; set; }
    }
}
