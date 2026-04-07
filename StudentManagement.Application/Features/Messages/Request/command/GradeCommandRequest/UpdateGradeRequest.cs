using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.command.GradeCommandRequest
{
    public class UpdateGradeRequest : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public float Score { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
