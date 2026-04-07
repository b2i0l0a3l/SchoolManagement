using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.AttendanceQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.AttendanceQueryHandler
{
    public class GetAllAttendancesQueryHandler : IRequestHandler<GetAllAttendancesRequest, Result<IEnumerable<AttendanceModel>>>
    {
        private readonly IRepository<Attendance> _Repo;
        public GetAllAttendancesQueryHandler(IRepository<Attendance> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<IEnumerable<AttendanceModel>>> Handle(GetAllAttendancesRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetAll();
            if (!result.IsSuccess || result.Value == null || !result.Value.Any())
                return result.Error!;
            IEnumerable<AttendanceModel> r = result.Value.Select(x => new AttendanceModel
            {
                Id = x.Id,
                StudentId = x.StudentId,
                Date = x.Date,
                Status = x.Status,
                Remarks = x.Remarks
            });
            return Result<IEnumerable<AttendanceModel>>.Success(r);
        }
    }
}
