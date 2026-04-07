using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Identity;
using StudentManagement.Application.Features.Messages.Request.command.StudentCommandRequest;
using StudentManagement.Application.Util;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.StudentCommandHandler
{
    public class AddNewStudentCommandHandler : IRequestHandler<AddNewStudentRequest, Result<StudentModel>>
    {
        private readonly IRepository<Student> _Repo;
        public AddNewStudentCommandHandler(IRepository<Student> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<StudentModel>> Handle(AddNewStudentRequest request, CancellationToken cancellationToken)
        {
            
            Student studentEntity = new ()
            {
                FullName = request.FullName,
                DateOfBirth = request.DateOfBirth,
                EnrollmentDate = request.EnrollmentDate,
                Gender = request.Gender,
                ClassId = request.ClassId
            };

            Result<Student> result = await _Repo.Add(studentEntity);
            if (!result.IsSuccess||result.Value == null)
                return result.Error!;

            
            StudentModel student = new ()
            {
                Id = result.Value.Id,
                FullName = result.Value.FullName,
                DateOfBirth = result.Value.DateOfBirth,
                EnrollmentDate = result.Value.EnrollmentDate,
                Gender = result.Value.Gender,
                ClassId = result.Value.ClassId
            };
            
            return student;
        }
    }
}
