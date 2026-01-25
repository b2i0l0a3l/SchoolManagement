using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.command.TeacherSubjectCommandRequest
{
    public class UpdateTeacherSubjectRequest : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int ClassId{ get; set; }
        public int SubjectId { get; set; }
    }
}
