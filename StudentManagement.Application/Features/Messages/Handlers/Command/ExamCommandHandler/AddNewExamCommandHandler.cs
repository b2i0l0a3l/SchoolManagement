using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.ExamCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.ExamCommandHandler
{
    public class AddNewExamCommandHandler : IRequestHandler<AddNewExamRequest, Result<ExamModel>>
    {
        private readonly IRepository<Exam> _Repo;
        public AddNewExamCommandHandler(IRepository<Exam> Repo) => _Repo = Repo;

        public async Task<Result<ExamModel>> Handle(AddNewExamRequest request, CancellationToken cancellationToken)
        {
            var entity = new Exam
            {
                Title = request.Title,
                Date = request.Date,
                SubjectId = request.SubjectId,
                MaxScore = request.MaxScore
            };

            Result<Exam> result = await _Repo.Add(entity);
            if (!result.IsSuccess) return result.Error!;

            return new ExamModel
            {
                Id = result.Value!.Id,
                Title = result.Value.Title,
                Date = result.Value.Date,
                SubjectId = result.Value.SubjectId,
                MaxScore = result.Value.MaxScore
            };
        }
    }
}
