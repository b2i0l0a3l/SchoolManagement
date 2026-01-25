using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private IMapper _Mapper;
        public AddNewClassCommandHandler(IRepository<Class> Repo,IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
            
        }
        public async Task<Result<ClassModel>> Handle(AddNewClassRequest request, CancellationToken cancellationToken)
        {
            Result<Class> result = await _Repo.Add(_Mapper.Map<Class>(request));
            if (!result.IsSuccess)
                return result.Error!;
            return _Mapper.Map<ClassModel>(result.Value);
        }
    }
}