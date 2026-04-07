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
    public class AddNewSubjectCommandHandler : IRequestHandler<AddNewSubjectRequest, Result<SubjectModel>>
    {
        private readonly IRepository<Subject> _Repo;
        public AddNewSubjectCommandHandler(IRepository<Subject> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<SubjectModel>> Handle(AddNewSubjectRequest request, CancellationToken cancellationToken)
        {
            var subjectEntity = new Subject
            {
                SubjectName = request.SubjectName,
                CreatAt = request.CreatAt
            };

            Result<Subject> result = await _Repo.Add(subjectEntity);
            if (!result.IsSuccess)
                return result.Error!;
            
            return new SubjectModel
            {
                Id = result.Value!.Id,
                SubjectName = result.Value.SubjectName,
                CreatAt = result.Value.CreatAt
            };
        }
    }
}
