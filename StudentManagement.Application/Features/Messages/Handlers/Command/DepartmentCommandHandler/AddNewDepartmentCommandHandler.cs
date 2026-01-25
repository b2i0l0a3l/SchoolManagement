using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.DepartmentCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.DepartmentCommandHandler
{
    public class AddNewDepartmentCommandHandler : IRequestHandler<AddNewDepartmentRequest, Result<DepartmentModel>>
    {
        private readonly IRepository<Department> _Repo;
        private IMapper _Mapper;
        public AddNewDepartmentCommandHandler(IRepository<Department> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
        }
        public async Task<Result<DepartmentModel>> Handle(AddNewDepartmentRequest request, CancellationToken cancellationToken)
        {
            Result<Department> result = await _Repo.Add(_Mapper.Map<Department>(request));
            if (!result.IsSuccess)
                return result.Error!;
            return _Mapper.Map<DepartmentModel>(result.Value);
        }
    }
}
