using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.ExamCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.ExamCommandHandler
{
    public class UpdateExamCommandHandler : IRequestHandler<UpdateExamRequest, Result<bool>>
    {
        private readonly IRepository<Exam> _Repo;
        public UpdateExamCommandHandler(IRepository<Exam> Repo) => _Repo = Repo;

        public async Task<Result<bool>> Handle(UpdateExamRequest request, CancellationToken cancellationToken)
        {
            var entityResult = await _Repo.GetById(request.Id);
            if (!entityResult.IsSuccess || entityResult.Value == null)
                return entityResult.IsSuccess ? Errors.UserNotFoundError : entityResult.Error!;


            Result<bool> res = await _Repo.Update(request.Id, x =>
            {
                x.Title = request.Title;
                x.Date = request.Date;
                x.SubjectId = request.SubjectId;
                x.MaxScore = request.MaxScore;

            });
            if (!res.IsSuccess) return res.Error!;
            return res.Value;
        }
    }
}
