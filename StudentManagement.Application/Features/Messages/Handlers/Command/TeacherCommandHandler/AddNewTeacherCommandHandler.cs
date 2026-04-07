using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Identity;
using StudentManagement.Application.Features.Messages.Request.command.TeacherCommandRequest;
using StudentManagement.Application.Util;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.TeacherCommandHandler
{
    public class AddNewTeacherCommandHandler : IRequestHandler<AddNewTeacherRequest, Result<TeacherModel>>
    {
        private readonly IRepository<Teacher> _Repo;
        public AddNewTeacherCommandHandler(IRepository<Teacher> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<TeacherModel>> Handle(AddNewTeacherRequest request, CancellationToken cancellationToken)
        {
    
            Teacher teacherEntity = new ()
            {
                FullName = request.FullName,
                HireDate = request.HireDate,
                DepartmentId = request.DepartmentId
            };

            Result<Teacher> result = await _Repo.Add(teacherEntity);
            return new TeacherModel
            {
                Id = result.Value!.Id,
                FullName = result.Value.FullName,
                HireDate = result.Value.HireDate,
                DepartmentId = result.Value.DepartmentId
            };
        }
    }
}
