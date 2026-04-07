using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.GradeCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.GradeCommandHandler
{
    public class AddNewGradeCommandHandler : IRequestHandler<AddNewGradeRequest, Result<GradeModel>>
    {
        private readonly IRepository<Grade> _Repo;
        public AddNewGradeCommandHandler(IRepository<Grade> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<GradeModel>> Handle(AddNewGradeRequest request, CancellationToken cancellationToken)
        {
            var gradeEntity = new Grade
            {
                StudentId = request.StudentId,
                ExamId = request.ExamId,
                Score = request.Score,
                CreateAt = request.CreateAt
            };

            Result<Grade> result = await _Repo.Add(gradeEntity);
            if (!result.IsSuccess)
                return result.Error!;
            
            return new GradeModel
            {
                Id = result.Value!.Id,
                StudentId = result.Value.StudentId,
                ExamId = result.Value.ExamId,
                Score = result.Value.Score,
                CreateAt = result.Value.CreateAt
            };
        }
    }
}
