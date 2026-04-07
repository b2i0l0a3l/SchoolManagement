using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.TeacherSubjectQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.TeacherSubjectQueryHandler
{
    public class GetTeacherSubjectByIdQueryHandler : IRequestHandler<GetTeacherSubjectByIdRequest, Result<TeacherSubjectModel>>
    {
        private readonly IRepository<TeacherSubject> _Repo;
        public GetTeacherSubjectByIdQueryHandler(IRepository<TeacherSubject> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<TeacherSubjectModel>> Handle(GetTeacherSubjectByIdRequest request, CancellationToken cancellationToken)
        {
            Result<TeacherSubject?> result = await _Repo.GetById(request.TeacherSubjectId);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;
            return new TeacherSubjectModel
            {
                Id = result.Value.Id,
                TeacherId = result.Value.TeacherId,
                SubjectId = result.Value.SubjectId,
                ClassId = result.Value.ClassId
            };
        }
    }
}
