using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;
using StudentManagement.Domain.common;

namespace StudentManagement.Application.Features.Messages.Request.Query.StudentQueryRequest
{
    public class GetAllStudentsRequest : IRequest<Result<PagedResult<StudentModel?>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
