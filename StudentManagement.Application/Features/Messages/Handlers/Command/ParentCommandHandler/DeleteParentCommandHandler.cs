using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.ParentCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.ParentCommandHandler
{
    public class DeleteParentCommandHandler : IRequestHandler<DeleteParentRequest, Result<bool>>
    {
        private readonly IRepository<Parent> _Repo;
        public DeleteParentCommandHandler(IRepository<Parent> Repo) => _Repo = Repo;

        public async Task<Result<bool>> Handle(DeleteParentRequest request, CancellationToken cancellationToken)
        {
            Result<bool> result = await _Repo.Delete(request.Id);
            if (!result.IsSuccess) return result.Error!;
            return result.Value;
        }
    }
}
