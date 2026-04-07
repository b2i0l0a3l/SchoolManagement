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
    public class AddNewTeacherSubjectCommandHandler : IRequestHandler<AddNewTeacherSubjectRequest, Result<TeacherSubjectModel>>
    {
        private readonly IRepository<TeacherSubject> _Repo;
        public AddNewTeacherSubjectCommandHandler(IRepository<TeacherSubject> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<TeacherSubjectModel>> Handle(AddNewTeacherSubjectRequest request, CancellationToken cancellationToken)
        {
            var teacherSubjectEntity = new TeacherSubject
            {
                TeacherId = request.TeacherId,
                ClassId = request.ClassId,
                SubjectId = request.SubjectId
            };

            Result<TeacherSubject> result = await _Repo.Add(teacherSubjectEntity);
            if (!result.IsSuccess)
                return result.Error!;
            
            return new TeacherSubjectModel
            {
                Id = result.Value!.Id,
                TeacherId = result.Value.TeacherId,
                SubjectId = result.Value.SubjectId,
                ClassId = result.Value.ClassId
            };
        }
    }
}
