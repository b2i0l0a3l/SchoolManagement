using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.command.DepartmentCommandRequest
{
    public class AddNewDepartmentRequest : IRequest<Result<DepartmentModel>>
    {
        public string Name { get; set; } = string.Empty;
    }
}
