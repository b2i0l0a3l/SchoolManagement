using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.Query.StudentQueryRequest
{
    public class GetStudentByIdRequest : IRequest<Result<StudentModel>>
    {
        public int StudentId { get; set; }
    }
}
