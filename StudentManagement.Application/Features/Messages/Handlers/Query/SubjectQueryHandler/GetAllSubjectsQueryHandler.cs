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
    public class GetAllSubjectsQueryHandler : IRequestHandler<GetAllSubjectsRequest,Result<IEnumerable<SubjectModel>>>
    {
        private readonly IRepository<Subject> _Repo;
        private readonly IMapper _Mapper;
        public GetAllSubjectsQueryHandler(IRepository<Subject> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
        }
        public async Task<Result<IEnumerable<SubjectModel>>> Handle(GetAllSubjectsRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetAll();
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;
            return result.Value.Select(x => _Mapper.Map<SubjectModel>(x)).ToList();
        }
    }
}
