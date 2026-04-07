using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.ClassQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.ClassQueryHandler
{
    public class GetClassStudentRequestHandler : IRequestHandler<GetClassStudentRequest, Result<ClassStudentModel>>
    {
        private readonly IRepository<Student> _StudentRepo;
        public GetClassStudentRequestHandler(IRepository<Student> StudentRepo)
        {
            _StudentRepo = StudentRepo;
        }
        public async Task<Result<ClassStudentModel>> Handle(GetClassStudentRequest request, CancellationToken cancellationToken)
        {
            Result<IEnumerable<Student>?> Student = await _StudentRepo.GetِAllByCondition(x => x.ClassId == request.ClassId);
            if (!Student.IsSuccess || Student.Value == null || !Student.Value.Any()) return Student.Error!;

            ClassStudentModel model = new()
            {
                ClassId = request.ClassId,
                Students = Student.Value.Select(x => new StudentModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    DateOfBirth = x.DateOfBirth,
                    EnrollmentDate = x.EnrollmentDate,
                    Gender = x.Gender,
                    ClassId = x.ClassId
                }).ToList()
            };
           
            return model;
        }
    }
}