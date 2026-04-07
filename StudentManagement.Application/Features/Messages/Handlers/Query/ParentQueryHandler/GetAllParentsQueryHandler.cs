using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.ParentQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.ParentQueryHandler
{
    public class GetAllParentsQueryHandler : IRequestHandler<GetAllParentsRequest, Result<IEnumerable<ParentModel>>>
    {
        private readonly IRepository<Parent> _Repo;
        public GetAllParentsQueryHandler(IRepository<Parent> Repo) => _Repo = Repo;

        public async Task<Result<IEnumerable<ParentModel>>> Handle(GetAllParentsRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetAll();
            if (!result.IsSuccess || result.Value == null || !result.Value.Any())
                return result.Error!;
            return result.Value.Select(x => new ParentModel
            {
                Id = x.Id, UserId = x.UserId,
                FullName = x.FullName, PhoneNumber = x.PhoneNumber
            }).ToList();
        }
    }
}
