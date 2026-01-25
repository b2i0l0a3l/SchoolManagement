using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.SubjectCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.SubjectCommandHandler
{
    public class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectRequest, Result<bool>>
    {
        private readonly IRepository<Subject> _Repo;
        public DeleteSubjectCommandHandler(IRepository<Subject> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<bool>> Handle(DeleteSubjectRequest request, CancellationToken cancellationToken)
        {
            Result<bool> result = await _Repo.Delete(request.Id);
            if (!result.IsSuccess)
                return result.Error!;
            return result.Value;
        }
    }
}
