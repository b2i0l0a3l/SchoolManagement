using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.ClassCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.ClassCommandHandler
{
    
    public class AddNewClassCommandHandler : IRequestHandler<AddNewClassRequest, Result<ClassModel>>


    {
        private readonly IRepository<Class> _Repo;
        public AddNewClassCommandHandler(IRepository<Class> Repo)
        {
            _Repo = Repo;
            
        }
        public async Task<Result<ClassModel>> Handle(AddNewClassRequest request, CancellationToken cancellationToken)
        {
            var classEntity = new Class
            {
                ClassName = request.ClassName,
                Year = request.Year
            };

            Result<Class> result = await _Repo.Add(classEntity);
            if (!result.IsSuccess)
                return result.Error!;
            
            return new ClassModel
            {
                Id = result.Value!.Id,
                ClassName = result.Value.ClassName,
                Year = result.Value.Year
            };
        }
    }
}