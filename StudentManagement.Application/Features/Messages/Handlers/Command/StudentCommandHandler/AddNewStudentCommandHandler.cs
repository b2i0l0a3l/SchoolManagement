using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly UserManager<User> _user;
        private IMapper _Mapper;
        private IUserRole _u;
        public AddNewStudentCommandHandler(UserManager<User> user,IUserRole u,IRepository<Student> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
            _u = u;
            _user = user;
        }
        public async Task<Result<StudentModel>> Handle(AddNewStudentRequest request, CancellationToken cancellationToken)
        {
            User? user = await _user.FindByIdAsync(request.UserId);
            if (user == null) return Errors.UserNotFoundError;
            Result<Student> result = await _Repo.Add(_Mapper.Map<Student>(request));
            if (!result.IsSuccess||result.Value == null)
                return result.Error!;
            Result<string> AccessToken = await _u.AddRoleToUserAndGenerateJwtKey(user, Roles.Student, (u, claims)=>{
                claims.Add(new Claim("StudentId", result.Value.Id.ToString()));
                return claims;
            });
            if (!AccessToken.IsSuccess || string.IsNullOrEmpty(AccessToken.Value)) return new Error("InternalServerError", ErrorType.General, "Error Happend By Generating Jwt Token.");
            StudentModel student = _Mapper.Map<StudentModel>(result.Value);
            return student;
        }
    }
}
