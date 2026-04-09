using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.ExamQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;
using StudentManagement.Domain.common;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.ExamQueryHandler
{
    public class GetAllExamsQueryHandler : IRequestHandler<GetAllExamsRequest, Result<PagedResult<ExamModel?>>>
    {
        private readonly IRepository<Exam> _Repo;
        public GetAllExamsQueryHandler(IRepository<Exam> Repo) => _Repo = Repo;

        public async Task<Result<PagedResult<ExamModel?>>> Handle(GetAllExamsRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetAll(request.PageNumber, request.PageSize);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;

            var mapped = new PagedResult<ExamModel?>
            {
                Items = result.Value.Items.Select(x => x == null ? null : new ExamModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Date = x.Date,
                    SubjectId = x.SubjectId,
                    MaxScore = x.MaxScore
                }),
                TotalItems = result.Value.TotalItems,
                PageNumber = result.Value.PageNumber,
                PageSize = result.Value.PageSize
            };
            return Result<PagedResult<ExamModel?>>.Success(mapped);
        }
    }
}
