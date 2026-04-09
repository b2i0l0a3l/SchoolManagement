using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.GradeQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;
using StudentManagement.Domain.common;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.GradeQueryHandler
{
    public class GetAllGradesQueryHandler : IRequestHandler<GetAllGradesRequest, Result<PagedResult<GradeModel?>>>
    {
        private readonly IRepository<Grade> _Repo;
        public GetAllGradesQueryHandler(IRepository<Grade> Repo) => _Repo = Repo;

        public async Task<Result<PagedResult<GradeModel?>>> Handle(GetAllGradesRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetAll(request.PageNumber, request.PageSize);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;

            var mapped = new PagedResult<GradeModel?>
            {
                Items = result.Value.Items.Select(x => x == null ? null : new GradeModel
                {
                    Id = x.Id,
                    StudentId = x.StudentId,
                    ExamId = x.ExamId,
                    Score = x.Score,
                    CreateAt = x.CreateAt
                }),
                TotalItems = result.Value.TotalItems,
                PageNumber = result.Value.PageNumber,
                PageSize = result.Value.PageSize
            };
            return Result<PagedResult<GradeModel?>>.Success(mapped);
        }
    }
}
