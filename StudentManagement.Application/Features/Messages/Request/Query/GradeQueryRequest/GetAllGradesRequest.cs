using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;
using StudentManagement.Domain.common;

namespace StudentManagement.Application.Features.Messages.Request.Query.GradeQueryRequest
{
    public class GetAllGradesRequest : IRequest<Result<PagedResult<GradeModel?>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
