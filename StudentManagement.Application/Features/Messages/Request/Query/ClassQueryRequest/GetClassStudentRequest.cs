using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.Query.ClassQueryRequest
{
    public class GetClassStudentRequest : IRequest<Result<ClassStudentModel>>
    {
        [Required]
        public int ClassId { get; set; }   
    }
}