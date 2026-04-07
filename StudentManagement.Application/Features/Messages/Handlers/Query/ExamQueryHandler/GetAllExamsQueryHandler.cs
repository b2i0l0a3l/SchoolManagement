using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.ExamQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.ExamQueryHandler
{
    public class GetAllExamsQueryHandler : IRequestHandler<GetAllExamsRequest, Result<IEnumerable<ExamModel>>>
    {
        private readonly IRepository<Exam> _Repo;
        public GetAllExamsQueryHandler(IRepository<Exam> Repo) => _Repo = Repo;

        public async Task<Result<IEnumerable<ExamModel>>> Handle(GetAllExamsRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetAll();
            if (!result.IsSuccess || result.Value == null || !result.Value.Any())
                return result.Error!;
            return result.Value.Select(x => new ExamModel
            {
                Id = x.Id, Title = x.Title, Date = x.Date,
                SubjectId = x.SubjectId, MaxScore = x.MaxScore
            }).ToList();
        }
    }
}
