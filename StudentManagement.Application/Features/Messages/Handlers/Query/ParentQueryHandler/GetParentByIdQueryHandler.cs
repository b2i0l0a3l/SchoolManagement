using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.ParentQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.ParentQueryHandler
{
    public class GetParentByIdQueryHandler : IRequestHandler<GetParentByIdRequest, Result<ParentModel>>
    {
        private readonly IRepository<Parent> _Repo;
        public GetParentByIdQueryHandler(IRepository<Parent> Repo) => _Repo = Repo;

        public async Task<Result<ParentModel>> Handle(GetParentByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetById(request.ParentId);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;
            return new ParentModel
            {
                Id = result.Value.Id, UserId = result.Value.UserId,
                FullName = result.Value.FullName, PhoneNumber = result.Value.PhoneNumber
            };
        }
    }
}
