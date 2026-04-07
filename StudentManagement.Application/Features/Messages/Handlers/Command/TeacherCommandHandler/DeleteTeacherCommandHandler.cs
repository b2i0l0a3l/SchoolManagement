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
    public class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherRequest, Result<bool>>
    {
        private readonly IRepository<Teacher> _Repo;
        public DeleteTeacherCommandHandler(IRepository<Teacher> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<bool>> Handle(DeleteTeacherRequest request, CancellationToken cancellationToken)
        {
            Result<bool> result = await _Repo.Delete(request.Id);
            if (!result.IsSuccess)
                return result.Error!;
            return result.Value;
        }
    }
}
