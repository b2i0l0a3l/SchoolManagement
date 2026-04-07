using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.ParentCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.ParentCommandHandler
{
    public class AddNewParentCommandHandler : IRequestHandler<AddNewParentRequest, Result<ParentModel>>
    {
        private readonly IRepository<Parent> _Repo;
        public AddNewParentCommandHandler(IRepository<Parent> Repo) => _Repo = Repo;

        public async Task<Result<ParentModel>> Handle(AddNewParentRequest request, CancellationToken cancellationToken)
        {
            var entity = new Parent
            {
                UserId = request.UserId,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber
            };

            Result<Parent> result = await _Repo.Add(entity);
            if (!result.IsSuccess) return result.Error!;

            return new ParentModel
            {
                Id = result.Value!.Id,
                UserId = result.Value.UserId,
                FullName = result.Value.FullName,
                PhoneNumber = result.Value.PhoneNumber
            };
        }
    }
}
