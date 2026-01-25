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
    public class UpdateGradeCommandHandler : IRequestHandler<UpdateGradeRequest, Result<bool>>
    {
        private readonly IRepository<Grade> _Repo;
        public UpdateGradeCommandHandler(IRepository<Grade> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<bool>> Handle(UpdateGradeRequest request, CancellationToken cancellationToken)
        {
            Result<bool> res = await _Repo.Update(request.Id, entity =>
            {
                entity.StudentId = request.StudentId;
                entity.SubjectId = request.SubjectId;
                entity.Score = request.Score;
                entity.CreateAt = request.CreateAt;
            });
            
            if (!res.IsSuccess)
                return res.Error!;
            return res.Value; 
        }
    }
}
