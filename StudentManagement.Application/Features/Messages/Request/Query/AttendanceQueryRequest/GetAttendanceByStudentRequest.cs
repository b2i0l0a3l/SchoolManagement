using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.Query.AttendanceQueryRequest
{
    public class GetAttendanceByStudentRequest : IRequest<Result<IEnumerable<AttendanceModel>>>
    {
        public int StudentId { get; set; }
    }
}
