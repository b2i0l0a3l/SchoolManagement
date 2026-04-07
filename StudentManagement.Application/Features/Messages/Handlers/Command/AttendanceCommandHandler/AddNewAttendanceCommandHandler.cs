using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.AttendanceCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.AttendanceCommandHandler
{
    public class AddNewAttendanceCommandHandler : IRequestHandler<AddNewAttendanceRequest, Result<AttendanceModel>>
    {
        private readonly IRepository<Attendance> _Repo;
        public AddNewAttendanceCommandHandler(IRepository<Attendance> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<AttendanceModel>> Handle(AddNewAttendanceRequest request, CancellationToken cancellationToken)
        {
            var entity = new Attendance
            {
                StudentId = request.StudentId,
                Date = request.Date,
                Status = request.Status,
                Remarks = request.Remarks
            };

            Result<Attendance> result = await _Repo.Add(entity);
            if (!result.IsSuccess)
                return result.Error!;

            return new AttendanceModel
            {
                Id = result.Value!.Id,
                StudentId = result.Value.StudentId,
                Date = result.Value.Date,
                Status = result.Value.Status,
                Remarks = result.Value.Remarks
            };
        }
    }
}
