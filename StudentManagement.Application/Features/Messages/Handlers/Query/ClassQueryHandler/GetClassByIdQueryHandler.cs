using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _Mapper;
        public GetClassByIdQueryHandler(IRepository<Class> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
        }

        public async Task<Result<ClassModel>> Handle(GetClassByIdRequest request, CancellationToken cancellationToken)
        {
            Result<Class?> result = await _Repo.GetById(request.ClassId);
            if (!result.IsSuccess|| result.Value == null)
                return result.Error!;

            return _Mapper.Map<ClassModel>(result.Value); 
        }
    }
}