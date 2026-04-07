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
    public class DeleteGradeCommandHandler : IRequestHandler<DeleteGradeRequest, Result<bool>>
    {
        private readonly IRepository<Grade> _Repo;
        public DeleteGradeCommandHandler(IRepository<Grade> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<bool>> Handle(DeleteGradeRequest request, CancellationToken cancellationToken)
        {
            Result<bool> result = await _Repo.Delete(request.Id);
            if (!result.IsSuccess)
                return result.Error!;
            return result.Value;
        }
    }
}
