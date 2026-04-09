using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.DepartmentQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;
using StudentManagement.Domain.common;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.DepartmentQueryHandler
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsRequest, Result<PagedResult<DepartmentModel?>>>
    {
        private readonly IRepository<Department> _Repo;
        public GetAllDepartmentsQueryHandler(IRepository<Department> Repo) => _Repo = Repo;

        public async Task<Result<PagedResult<DepartmentModel?>>> Handle(GetAllDepartmentsRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetAll(request.PageNumber, request.PageSize);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;

            var mapped = new PagedResult<DepartmentModel?>
            {
                Items = result.Value.Items.Select(x => x == null ? null : new DepartmentModel
                {
                    Id = x.Id,
                    Name = x.Name
                }),
                TotalItems = result.Value.TotalItems,
                PageNumber = result.Value.PageNumber,
                PageSize = result.Value.PageSize
            };
            return Result<PagedResult<DepartmentModel?>>.Success(mapped);
        }
    }
}
