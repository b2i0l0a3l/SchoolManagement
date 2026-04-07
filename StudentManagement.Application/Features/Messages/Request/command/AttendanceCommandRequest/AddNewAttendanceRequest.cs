using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.enums;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.command.AttendanceCommandRequest
{
    public class AddNewAttendanceRequest : IRequest<Result<AttendanceModel>>
    {
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }
        public string Remarks { get; set; } = string.Empty;
    }
}
