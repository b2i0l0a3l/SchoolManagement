using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.ParentQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;
using StudentManagement.Domain.common;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.ParentQueryHandler
{
    public class GetAllParentsQueryHandler : IRequestHandler<GetAllParentsRequest, Result<PagedResult<ParentModel?>>>
    {
        private readonly IRepository<Parent> _Repo;
        public GetAllParentsQueryHandler(IRepository<Parent> Repo) => _Repo = Repo;

        public async Task<Result<PagedResult<ParentModel?>>> Handle(GetAllParentsRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetAll(request.PageNumber, request.PageSize);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;

            var mapped = new PagedResult<ParentModel?>
            {
                Items = result.Value.Items.Select(x => x == null ? null : new ParentModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    FullName = x.FullName,
                    PhoneNumber = x.PhoneNumber
                }),
                TotalItems = result.Value.TotalItems,
                PageNumber = result.Value.PageNumber,
                PageSize = result.Value.PageSize
            };
            return Result<PagedResult<ParentModel?>>.Success(mapped);
        }
    }
}
