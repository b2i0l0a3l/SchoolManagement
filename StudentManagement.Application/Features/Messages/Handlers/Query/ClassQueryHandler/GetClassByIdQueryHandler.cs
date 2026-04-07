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
    public class GetClassByIdQueryHandler : IRequestHandler<GetClassByIdRequest, Result<ClassModel>>
    {
        private readonly IRepository<Class> _Repo;
        public GetClassByIdQueryHandler(IRepository<Class> Repo)
        {
            _Repo = Repo;
        }

        public async Task<Result<ClassModel>> Handle(GetClassByIdRequest request, CancellationToken cancellationToken)
        {
            Result<Class?> result = await _Repo.GetById(request.ClassId);
            if (!result.IsSuccess|| result.Value == null)
                return result.Error!;

            return new ClassModel
            {
                Id = result.Value.Id,
                ClassName = result.Value.ClassName,
                Year = result.Value.Year
            }; 
        }
    }
}