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
    public class AddNewSubjectCommandHandler : IRequestHandler<AddNewSubjectRequest, Result<SubjectModel>>
    {
        private readonly IRepository<Subject> _Repo;
        private IMapper _Mapper;
        public AddNewSubjectCommandHandler(IRepository<Subject> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
        }
        public async Task<Result<SubjectModel>> Handle(AddNewSubjectRequest request, CancellationToken cancellationToken)
        {
            Result<Subject> result = await _Repo.Add(_Mapper.Map<Subject>(request));
            if (!result.IsSuccess)
                return result.Error!;
            return _Mapper.Map<SubjectModel>(result.Value);
        }
    }
}
