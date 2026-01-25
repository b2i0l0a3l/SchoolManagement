using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.SubjectCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.SubjectCommandHandler
{
    public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectRequest, Result<bool>>
    {
        private readonly IRepository<Subject> _Repo;
        public UpdateSubjectCommandHandler(IRepository<Subject> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<bool>> Handle(UpdateSubjectRequest request, CancellationToken cancellationToken)
        {
            Result<bool> res = await _Repo.Update(request.Id, entity =>
            {
                entity.SubjectName = request.SubjectName;
                entity.CreatAt = request.CreatAt;
            });
            
            if (!res.IsSuccess)
                return res.Error!;
            return res.Value; 
        }
    }
}
