using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.StudentQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;
using StudentManagement.Domain.common;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.StudentQueryHandler
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsRequest, Result<PagedResult<StudentModel?>>>
    {
        private readonly IRepository<Student> _Repo;
        public GetAllStudentsQueryHandler(IRepository<Student> Repo) => _Repo = Repo;

        public async Task<Result<PagedResult<StudentModel?>>> Handle(GetAllStudentsRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetAll(request.PageNumber, request.PageSize);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;

            var mapped = new PagedResult<StudentModel?>
            {
                Items = result.Value.Items.Select(x => x == null ? null : new StudentModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    DateOfBirth = x.DateOfBirth,
                    EnrollmentDate = x.EnrollmentDate,
                    Gender = x.Gender,
                    ClassId = x.ClassId
                }),
                TotalItems = result.Value.TotalItems,
                PageNumber = result.Value.PageNumber,
                PageSize = result.Value.PageSize
            };
            return Result<PagedResult<StudentModel?>>.Success(mapped);
        }
    }
}
