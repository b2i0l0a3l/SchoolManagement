using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.AttendanceCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.AttendanceCommandHandler
{
    public class DeleteAttendanceCommandHandler : IRequestHandler<DeleteAttendanceRequest, Result<bool>>
    {
        private readonly IRepository<Attendance> _Repo;
        public DeleteAttendanceCommandHandler(IRepository<Attendance> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<bool>> Handle(DeleteAttendanceRequest request, CancellationToken cancellationToken)
        {
            Result<bool> result = await _Repo.Delete(request.Id);
            if (!result.IsSuccess)
                return result.Error!;
            return result.Value;
        }
    }
}
