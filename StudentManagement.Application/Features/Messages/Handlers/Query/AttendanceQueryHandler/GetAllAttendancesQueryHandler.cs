using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.AttendanceQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;
using StudentManagement.Domain.common;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.AttendanceQueryHandler
{
    public class GetAllAttendancesQueryHandler : IRequestHandler<GetAllAttendancesRequest, Result<PagedResult<AttendanceModel?>>>
    {
        private readonly IRepository<Attendance> _Repo;
        public GetAllAttendancesQueryHandler(IRepository<Attendance> Repo) => _Repo = Repo;

        public async Task<Result<PagedResult<AttendanceModel?>>> Handle(GetAllAttendancesRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetAll(request.PageNumber, request.PageSize);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;

            var mapped = new PagedResult<AttendanceModel?>
            {
                Items = result.Value.Items.Select(x => x == null ? null : new AttendanceModel
                {
                    Id = x.Id,
                    StudentId = x.StudentId,
                    Date = x.Date,
                    Status = x.Status,
                    Remarks = x.Remarks
                }),
                TotalItems = result.Value.TotalItems,
                PageNumber = result.Value.PageNumber,
                PageSize = result.Value.PageSize
            };
            return Result<PagedResult<AttendanceModel?>>.Success(mapped);
        }
    }
}
