using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.ClassCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.ClassCommandHandler
{
    public class DeleteClassCommandHandler : IRequestHandler<DeleteClassRequest, Result<bool>>
    {
        private readonly IRepository<Class> _Repo;
        public DeleteClassCommandHandler(IRepository<Class> Repo)
        {
            _Repo = Repo;

        }
        public async Task<Result<bool>> Handle(DeleteClassRequest request, CancellationToken cancellationToken)
        {
            Result<bool> result = await _Repo.Delete(request.Id);
            if (!result.IsSuccess)
                return result.Error!;
            return result.Value; 
        }
    }
}