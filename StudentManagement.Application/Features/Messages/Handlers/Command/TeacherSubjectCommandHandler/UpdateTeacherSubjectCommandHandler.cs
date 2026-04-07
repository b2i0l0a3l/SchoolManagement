using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.TeacherSubjectCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.TeacherSubjectCommandHandler
{
    public class UpdateTeacherSubjectCommandHandler : IRequestHandler<UpdateTeacherSubjectRequest, Result<bool>>
    {
        private readonly IRepository<TeacherSubject> _Repo;
        public UpdateTeacherSubjectCommandHandler(IRepository<TeacherSubject> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<bool>> Handle(UpdateTeacherSubjectRequest request, CancellationToken cancellationToken)
        {
            var entityResult = await _Repo.GetById(request.Id);
            if (!entityResult.IsSuccess || entityResult.Value == null)
                return entityResult.IsSuccess ? Errors.UserNotFoundError : entityResult.Error!;


            Result<bool> res = await _Repo.Update(request.Id, x =>
            {
                x.TeacherId = request.TeacherId;
                x.ClassId = request.ClassId;
                x.SubjectId = request.SubjectId;
            });

            if (!res.IsSuccess)
                return res.Error!;
            return res.Value; 
        }
    }
}
