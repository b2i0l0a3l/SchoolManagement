using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.ExamQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.ExamQueryHandler
{
    public class GetExamByIdQueryHandler : IRequestHandler<GetExamByIdRequest, Result<ExamModel>>
    {
        private readonly IRepository<Exam> _Repo;
        public GetExamByIdQueryHandler(IRepository<Exam> Repo) => _Repo = Repo;

        public async Task<Result<ExamModel>> Handle(GetExamByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetById(request.ExamId);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;
            return new ExamModel
            {
                Id = result.Value.Id, Title = result.Value.Title,
                Date = result.Value.Date, SubjectId = result.Value.SubjectId,
                MaxScore = result.Value.MaxScore
            };
        }
    }
}
