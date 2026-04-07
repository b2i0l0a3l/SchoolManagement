using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.ClassQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.ClassQueryHandler
{
    public class GetAllClassesQueryHandler : IRequestHandler<GetAllClassesRequest,Result<IEnumerable<ClassModel>>>
    {
        private readonly IRepository<Class> _Repo;
        public GetAllClassesQueryHandler(IRepository<Class> Repo)
        {
            _Repo = Repo;
        }

        public async Task<Result<IEnumerable<ClassModel>>> Handle(GetAllClassesRequest request, CancellationToken cancellationToken)
        {
            Result<IEnumerable<Class>?> result = await _Repo.GetAll();
            if (!result.IsSuccess|| result.Value == null || !result.Value.Any())
                return result.Error!;

            IEnumerable<ClassModel> r = result.Value.Select(x => new ClassModel
            {
                Id = x.Id,
                ClassName = x.ClassName,
                Year = x.Year
            });
            return Result<IEnumerable<ClassModel>>.Success(r);; 
        }
    }
}