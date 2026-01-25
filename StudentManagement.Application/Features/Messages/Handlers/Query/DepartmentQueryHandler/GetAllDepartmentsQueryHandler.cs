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
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsRequest, Result<IEnumerable<DepartmentModel>>>
    {
        private readonly IRepository<Department> _Repo;
        private readonly IMapper _Mapper;
        public GetAllDepartmentsQueryHandler(IRepository<Department> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
        }
        public async Task<Result<IEnumerable<DepartmentModel>>> Handle(GetAllDepartmentsRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetAll();
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;
            return result.Value.Select(x => _Mapper.Map<DepartmentModel>(x)).ToList();
        }
    }
}
