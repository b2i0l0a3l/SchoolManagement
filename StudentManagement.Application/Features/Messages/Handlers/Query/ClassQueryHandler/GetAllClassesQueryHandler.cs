using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.ClassQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;
using StudentManagement.Domain.common;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.ClassQueryHandler
{
    public class GetAllClassesQueryHandler : IRequestHandler<GetAllClassesRequest, Result<PagedResult<ClassModel?>>>
    {
        private readonly IRepository<Class> _Repo;
        public GetAllClassesQueryHandler(IRepository<Class> Repo) => _Repo = Repo;

        public async Task<Result<PagedResult<ClassModel?>>> Handle(GetAllClassesRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetAll(request.PageNumber, request.PageSize);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;

            var mapped = new PagedResult<ClassModel?>
            {
                Items = result.Value.Items.Select(x => x == null ? null : new ClassModel
                {
                    Id = x.Id,
                    ClassName = x.ClassName,
                    Year = x.Year
                }),
                TotalItems = result.Value.TotalItems,
                PageNumber = result.Value.PageNumber,
                PageSize = result.Value.PageSize
            };
            return Result<PagedResult<ClassModel?>>.Success(mapped);
        }
    }
}