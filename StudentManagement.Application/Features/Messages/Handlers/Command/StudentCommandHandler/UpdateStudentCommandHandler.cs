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
            Result<bool> res = await _Repo.Update(request.Id, entity =>
            {
                entity.FullName  = request.FullName;
                entity.DateOfBirth = request.DateOfBirth;
                entity.EnrollmentDate = request.EnrollmentDate;
                entity.Gender = request.Gender;
                entity.ClassId = request.ClassId;
            });
            
            if (!res.IsSuccess)
                return res.Error!;
            return res.Value; 
        }
    }
}
