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
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentRequest, Result<bool>>
    {
        private readonly IRepository<Department> _Repo;
        public UpdateDepartmentCommandHandler(IRepository<Department> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<bool>> Handle(UpdateDepartmentRequest request, CancellationToken cancellationToken)
        {
            var entityResult = await _Repo.GetById(request.Id);
            if (!entityResult.IsSuccess || entityResult.Value == null)
                return entityResult.IsSuccess ? Errors.UserNotFoundError : entityResult.Error!;


            Result<bool> res = await _Repo.Update(request.Id, x =>
            {
                x.Name = request.Name;
            });
            
            if (!res.IsSuccess)
                return res.Error!;
            return res.Value; 
        }
    }
}
