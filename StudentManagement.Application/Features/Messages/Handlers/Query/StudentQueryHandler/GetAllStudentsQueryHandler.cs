using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.StudentQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.StudentQueryHandler
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsRequest, Result<IEnumerable<StudentModel>>>
    {
        private readonly IRepository<Student> _Repo;
        public GetAllStudentsQueryHandler(IRepository<Student> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<IEnumerable<StudentModel>>> Handle(GetAllStudentsRequest request, CancellationToken cancellationToken)
        {
            Result<IEnumerable<Student>?> result = await _Repo.GetAll();
            if (!result.IsSuccess || result.Value == null || !result.Value.Any())
                return result.Error!;
            IEnumerable<StudentModel> r = result.Value.Select(x => new StudentModel
            {
                Id = x.Id,
                FullName = x.FullName,
                DateOfBirth = x.DateOfBirth,
                EnrollmentDate = x.EnrollmentDate,
                Gender = x.Gender,
                ClassId = x.ClassId
            });
            return Result<IEnumerable<StudentModel>>.Success(r);
        }
    }
}
