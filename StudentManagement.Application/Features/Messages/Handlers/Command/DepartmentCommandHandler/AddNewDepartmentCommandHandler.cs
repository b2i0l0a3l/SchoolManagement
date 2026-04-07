using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.DepartmentCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.DepartmentCommandHandler
{
    public class AddNewDepartmentCommandHandler : IRequestHandler<AddNewDepartmentRequest, Result<DepartmentModel>>
    {
        private readonly IRepository<Department> _Repo;
        public AddNewDepartmentCommandHandler(IRepository<Department> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<DepartmentModel>> Handle(AddNewDepartmentRequest request, CancellationToken cancellationToken)
        {
            var dept = new Department
            {
                Name = request.Name
            };

            Result<Department> result = await _Repo.Add(dept);
            if (!result.IsSuccess)
                return result.Error!;
            
            return new DepartmentModel
            {
                Id = result.Value!.Id,
                Name = result.Value.Name
            };
        }
    }
}
