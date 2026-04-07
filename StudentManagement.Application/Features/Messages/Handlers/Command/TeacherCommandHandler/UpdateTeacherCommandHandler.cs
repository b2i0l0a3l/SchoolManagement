using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.TeacherCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.TeacherCommandHandler
{
    public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherRequest, Result<bool>>
    {
        private readonly IRepository<Teacher> _Repo;
        public UpdateTeacherCommandHandler(IRepository<Teacher> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<bool>> Handle(UpdateTeacherRequest request, CancellationToken cancellationToken)
        {
            var entityResult = await _Repo.GetById(request.Id);
            if (!entityResult.IsSuccess || entityResult.Value == null)
                return entityResult.IsSuccess ? Errors.UserNotFoundError : entityResult.Error!;


            Result<bool> res = await _Repo.Update(request.Id, x =>
            {
                x.FullName = request.FullName;
                x.HireDate = request.HireDate;
                x.DepartmentId = request.DepartmentId;
            });
            
            if (!res.IsSuccess)
                return res.Error!;
            return res.Value; 
        }
    }
}
