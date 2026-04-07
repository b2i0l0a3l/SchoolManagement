using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.GradeQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.GradeQueryHandler
{
    public class GetGradeByIdQueryHandler : IRequestHandler<GetGradeByIdRequest, Result<GradeModel>>
    {
        private readonly IRepository<Grade> _Repo;
        public GetGradeByIdQueryHandler(IRepository<Grade> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<GradeModel>> Handle(GetGradeByIdRequest request, CancellationToken cancellationToken)
        {
            Result<Grade?> result = await _Repo.GetById(request.GradeId);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;
            return new GradeModel
            {
                Id = result.Value.Id,
                StudentId = result.Value.StudentId,
                ExamId = result.Value.ExamId,
                Score = result.Value.Score,
                CreateAt = result.Value.CreateAt
            };
        }
    }
}
