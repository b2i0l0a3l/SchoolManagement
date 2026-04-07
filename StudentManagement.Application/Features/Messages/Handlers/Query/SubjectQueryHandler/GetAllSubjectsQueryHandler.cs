using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public GetAllSubjectsQueryHandler(IRepository<Subject> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<IEnumerable<SubjectModel>>> Handle(GetAllSubjectsRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetAll();
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;
            return result.Value.Select(x => new SubjectModel
            {
                Id = x.Id,
                SubjectName = x.SubjectName,
                CreatAt = x.CreatAt
            }).ToList();
        }
    }
}
