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
            Result<bool> res = await _Repo.Update(request.Id, entity =>
            {
                entity.FullName = request.FullName;
                entity.HireDate = request.HireDate;
                entity.DepartmentId = request.DepartmentId;
            });
            
            if (!res.IsSuccess)
                return res.Error!;
            return res.Value; 
        }
    }
}
