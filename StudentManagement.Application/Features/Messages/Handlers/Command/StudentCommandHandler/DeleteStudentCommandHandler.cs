using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.StudentCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.StudentCommandHandler
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentRequest, Result<bool>>
    {
        private readonly IRepository<Student> _Repo;
        public DeleteStudentCommandHandler(IRepository<Student> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<bool>> Handle(DeleteStudentRequest request, CancellationToken cancellationToken)
        {
            Result<bool> result = await _Repo.Delete(request.Id);
            if (!result.IsSuccess)
                return result.Error!;
            return result.Value;
        }
    }
}
