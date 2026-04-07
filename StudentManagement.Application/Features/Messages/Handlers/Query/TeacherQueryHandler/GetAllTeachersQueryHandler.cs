using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.TeacherQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.TeacherQueryHandler
{
    public class GetAllTeachersQueryHandler : IRequestHandler<GetAllTeachersRequest, Result<IEnumerable<TeacherModel>>>
    {
        private readonly IRepository<Teacher> _Repo;
        public GetAllTeachersQueryHandler(IRepository<Teacher> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<IEnumerable<TeacherModel>>> Handle(GetAllTeachersRequest request, CancellationToken cancellationToken)
        {
            Result<IEnumerable<Teacher>?> result = await _Repo.GetAll();
            if (!result.IsSuccess || result.Value == null || !result.Value.Any())
                return result.Error!;
            IEnumerable<TeacherModel> r = result.Value.Select(x => new TeacherModel
            {
                Id = x.Id,
                FullName = x.FullName,
                HireDate = x.HireDate,
                DepartmentId = x.DepartmentId
            });
            return Result<IEnumerable<TeacherModel>>.Success(r);
        }
    }
}
