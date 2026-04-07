using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.ExamCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.ExamCommandHandler
{
    public class DeleteExamCommandHandler : IRequestHandler<DeleteExamRequest, Result<bool>>
    {
        private readonly IRepository<Exam> _Repo;
        public DeleteExamCommandHandler(IRepository<Exam> Repo) => _Repo = Repo;

        public async Task<Result<bool>> Handle(DeleteExamRequest request, CancellationToken cancellationToken)
        {
            Result<bool> result = await _Repo.Delete(request.Id);
            if (!result.IsSuccess) return result.Error!;
            return result.Value;
        }
    }
}
