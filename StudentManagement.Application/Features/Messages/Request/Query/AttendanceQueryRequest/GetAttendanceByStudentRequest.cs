using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;
using StudentManagement.Domain.common;

namespace StudentManagement.Application.Features.Messages.Request.Query.AttendanceQueryRequest
{
    public class GetAttendanceByStudentRequest : IRequest<Result<PagedResult<AttendanceModel?>>>
    {
        public int StudentId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
