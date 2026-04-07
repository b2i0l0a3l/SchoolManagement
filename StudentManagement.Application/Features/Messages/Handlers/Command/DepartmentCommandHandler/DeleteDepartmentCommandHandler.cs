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
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentRequest, Result<bool>>
    {
        private readonly IRepository<Department> _Repo;
        public DeleteDepartmentCommandHandler(IRepository<Department> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<bool>> Handle(DeleteDepartmentRequest request, CancellationToken cancellationToken)
        {
            Result<bool> result = await _Repo.Delete(request.Id);
            if (!result.IsSuccess)
                return result.Error!;
            return result.Value;
        }
    }
}
