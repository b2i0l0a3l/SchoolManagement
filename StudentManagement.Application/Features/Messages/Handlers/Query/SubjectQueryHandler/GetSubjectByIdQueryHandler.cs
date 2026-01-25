using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.SubjectQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.SubjectQueryHandler
{
    public class GetSubjectByIdQueryHandler : IRequestHandler<GetSubjectByIdRequest, Result<SubjectModel>>
    {
        private readonly IRepository<Subject> _Repo;
        private readonly IMapper _Mapper;
        public GetSubjectByIdQueryHandler(IRepository<Subject> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
        }
        public async Task<Result<SubjectModel>> Handle(GetSubjectByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetById(request.SubjectId);
            if (!result.IsSuccess || result.Value == null)
                return new SubjectModel();
            return _Mapper.Map<SubjectModel>(result.Value);
        }
    }
}
