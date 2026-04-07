using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public GetAllDepartmentsQueryHandler(IRepository<Department> Repo)
        {
            _Repo = Repo;
        }
        public async Task<Result<IEnumerable<DepartmentModel>>> Handle(GetAllDepartmentsRequest request, CancellationToken cancellationToken)
        {
            var result = await _Repo.GetAll();
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;
            return result.Value.Select(x => new DepartmentModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
