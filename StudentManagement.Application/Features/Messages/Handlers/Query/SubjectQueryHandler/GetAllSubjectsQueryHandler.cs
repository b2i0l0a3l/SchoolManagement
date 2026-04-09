using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.SubjectQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;
using StudentManagement.Domain.common;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.SubjectQueryHandler
{
    public class GetAllSubjectsQueryHandler : IRequestHandler<GetAllSubjectsRequest, Result<PagedResult<SubjectModel?>>>
    {
        private readonly IRepository<Subject> _Repo;
        public GetAllSubjectsQueryHandler(IRepository<Subject> Repo) => _Repo = Repo;

        public async Task<Result<PagedResult<SubjectModel?>>> Handle(GetAllSubjectsRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetAll(request.PageNumber, request.PageSize);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;

            var mapped = new PagedResult<SubjectModel?>
            {
                Items = result.Value.Items.Select(x => x == null ? null : new SubjectModel
                {
                    Id = x.Id,
                    SubjectName = x.SubjectName,
                    CreatAt = x.CreatAt
                }),
                TotalItems = result.Value.TotalItems,
                PageNumber = result.Value.PageNumber,
                PageSize = result.Value.PageSize
            };
            return Result<PagedResult<SubjectModel?>>.Success(mapped);
        }
    }
}
