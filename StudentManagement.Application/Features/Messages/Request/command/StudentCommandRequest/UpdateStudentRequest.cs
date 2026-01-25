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
    public class UpdateStudentRequest : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public enGender Gender { get; set; } 
        public int ClassId { get; set; }
    }
}
