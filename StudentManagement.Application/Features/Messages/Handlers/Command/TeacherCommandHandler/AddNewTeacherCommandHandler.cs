using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private IMapper _Mapper;
        private readonly UserManager<User> _user;
        private IUserRole _u;

        public AddNewTeacherCommandHandler(UserManager<User> user,IUserRole u,IRepository<Teacher> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
            _u = u;
            _user = user;

        }
        public async Task<Result<TeacherModel>> Handle(AddNewTeacherRequest request, CancellationToken cancellationToken)
        {
            User? user = await _user.FindByIdAsync(request.UserId);
            if (user == null) return Errors.UserNotFoundError;
            Result<Teacher> result = await _Repo.Add(_Mapper.Map<Teacher>(request));
            if (!result.IsSuccess)
                return result.Error!;
            Result<string> AccessToken = await _u.AddRoleToUserAndGenerateJwtKey(user, Roles.Teacher);
            if (!AccessToken.IsSuccess || string.IsNullOrEmpty(AccessToken.Value)) return new Error("InternalServerError", ErrorType.General, "Error Happend By Generating Jwt Token.");
            return  _Mapper.Map<TeacherModel>(result.Value) ;
        }
    }
}
