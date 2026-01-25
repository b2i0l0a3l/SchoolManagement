using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.DepartmentQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.DepartmentQueryHandler
{
    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdRequest,Result<DepartmentModel>>
    {
        private readonly IRepository<Department> _Repo;
        private readonly IMapper _Mapper;
        public GetDepartmentByIdQueryHandler(IRepository<Department> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
        }
        public async Task<Result<DepartmentModel>> Handle(GetDepartmentByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetById(request.DepartmentId);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;
            return _Mapper.Map<DepartmentModel>(result.Value);
        }
    }
}
