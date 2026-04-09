using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.TeacherSubjectQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;
using StudentManagement.Domain.common;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.TeacherSubjectQueryHandler
{
    public class GetAllTeacherSubjectsQueryHandler : IRequestHandler<GetAllTeacherSubjectsRequest, Result<PagedResult<TeacherSubjectModel?>>>
    {
        private readonly IRepository<TeacherSubject> _Repo;
        public GetAllTeacherSubjectsQueryHandler(IRepository<TeacherSubject> Repo) => _Repo = Repo;

        public async Task<Result<PagedResult<TeacherSubjectModel?>>> Handle(GetAllTeacherSubjectsRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetAll(request.PageNumber, request.PageSize);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;

            var mapped = new PagedResult<TeacherSubjectModel?>
            {
                Items = result.Value.Items.Select(x => x == null ? null : new TeacherSubjectModel
                {
                    Id = x.Id,
                    TeacherId = x.TeacherId,
                    SubjectId = x.SubjectId,
                    ClassId = x.ClassId
                }),
                TotalItems = result.Value.TotalItems,
                PageNumber = result.Value.PageNumber,
                PageSize = result.Value.PageSize
            };
            return Result<PagedResult<TeacherSubjectModel?>>.Success(mapped);
        }
    }
}
