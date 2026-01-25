using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StudentManagement.Application.Features.Messages.Request.command.Register;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.RegisterCommandHandler
{
    public class RegisterRequestHandler : IRequestHandler<RegisterRequest, Result<RegisterModel>>
    {
        private readonly UserManager<User> _manager;
        public RegisterRequestHandler(UserManager<User> manager)
        {
            _manager = manager;
        } 
        public async Task<Result<RegisterModel>> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            User? existing = await _manager.FindByEmailAsync(request.Email);
            if (existing != null) return Errors.EmailAlreadyExistsError;

            User user = new()
            {

                UserName = request.Email,
                Role = Roles.Admin,
                Email = request.Email
            };
            
            var result = await _manager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return new Error("CreateUserError", ErrorType.General, string.Join(", ", result.Errors.Select(e =>e.Description)));
            return new RegisterModel() {Id= user.Id, Email = user.Email, Role = user.Role };
        }
    }
}