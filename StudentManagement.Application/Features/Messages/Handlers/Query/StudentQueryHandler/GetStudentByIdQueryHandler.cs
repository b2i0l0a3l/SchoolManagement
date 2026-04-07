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
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdRequest, Result<StudentModel>>
    {
        private readonly IRepository<Student> _Repo;
        public GetStudentByIdQueryHandler(IRepository<Student> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<StudentModel>> Handle(GetStudentByIdRequest request, CancellationToken cancellationToken)
        {
            Result<Student?> result = await _Repo.GetById(request.StudentId);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;
            return new StudentModel
            {
                Id = result.Value.Id,
                FullName = result.Value.FullName,
                DateOfBirth = result.Value.DateOfBirth,
                EnrollmentDate = result.Value.EnrollmentDate,
                Gender = result.Value.Gender,
                ClassId = result.Value.ClassId
            };
        }
    }
}
