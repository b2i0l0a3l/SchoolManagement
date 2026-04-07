using MediatR;
using StudentManagement.Domain.Common;

namespace StudentManagement.Application.Features.Messages.Request.command.AttendanceCommandRequest
{
    public class DeleteAttendanceRequest : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }
}
