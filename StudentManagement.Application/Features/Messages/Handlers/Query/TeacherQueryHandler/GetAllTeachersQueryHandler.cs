using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.TeacherQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;
using StudentManagement.Domain.common;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.TeacherQueryHandler
{
    public class GetAllTeachersQueryHandler : IRequestHandler<GetAllTeachersRequest, Result<PagedResult<TeacherModel?>>>
    {
        private readonly IRepository<Teacher> _Repo;
        public GetAllTeachersQueryHandler(IRepository<Teacher> Repo) => _Repo = Repo;

        public async Task<Result<PagedResult<TeacherModel?>>> Handle(GetAllTeachersRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetAll(request.PageNumber, request.PageSize);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;

            var mapped = new PagedResult<TeacherModel?>
            {
                Items = result.Value.Items.Select(x => x == null ? null : new TeacherModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    HireDate = x.HireDate,
                    DepartmentId = x.DepartmentId
                }),
                TotalItems = result.Value.TotalItems,
                PageNumber = result.Value.PageNumber,
                PageSize = result.Value.PageSize
            };
            return Result<PagedResult<TeacherModel?>>.Success(mapped);
        }
    }
}
