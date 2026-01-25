using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.Query.StudentQueryRequest
{
    public class GetPassedStudentByClassIdQueryRequest : IRequest<Result<IEnumerable<StudentGradeModel>>>
    {
        public int ClassId { get; set; }
    }
}