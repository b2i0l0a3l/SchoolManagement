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
    public class DeleteTeacherSubjectCommandHandler : IRequestHandler<DeleteTeacherSubjectRequest, Result<bool>>
    {
        private readonly IRepository<TeacherSubject> _Repo;
        public DeleteTeacherSubjectCommandHandler(IRepository<TeacherSubject> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<bool>> Handle(DeleteTeacherSubjectRequest request, CancellationToken cancellationToken)
        {
            Result<bool> result = await _Repo.Delete(request.Id);
            if (!result.IsSuccess)
                return result.Error!;
            return result.Value;
        }
    }
}
