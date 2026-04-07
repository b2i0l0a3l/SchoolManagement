using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.StudentCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.StudentCommandHandler
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentRequest, Result<bool>>
    {
        private readonly IRepository<Student> _Repo;
        public UpdateStudentCommandHandler(IRepository<Student> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<bool>> Handle(UpdateStudentRequest request, CancellationToken cancellationToken)
        {
            Result<Student?> entityResult = await _Repo.GetById(request.Id);
            if (!entityResult.IsSuccess || entityResult.Value == null)
                return entityResult.IsSuccess ? Errors.UserNotFoundError : entityResult.Error!;

          

            Result<bool> res = await _Repo.Update(request.Id, x =>
            {
                x.FullName = request.FullName;
                x.DateOfBirth = request.DateOfBirth;
                x.EnrollmentDate = request.EnrollmentDate;
                x.Gender = request.Gender;
                x.ClassId = request.ClassId;
                x.DateOfBirth = request.DateOfBirth;

            });
        
            if (!res.IsSuccess)
                return res.Error!;
            return res.Value; 
        }
    }
}
