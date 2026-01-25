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
    public class GetAllClassesQueryHandler : IRequestHandler<GetAllClassesRequest,Result<IEnumerable<ClassModel>>>
    {
        private readonly IRepository<Class> _Repo;
        private readonly IMapper _Mapper;
        public GetAllClassesQueryHandler(IRepository<Class> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
        }

        public async Task<Result<IEnumerable<ClassModel>>> Handle(GetAllClassesRequest request, CancellationToken cancellationToken)
        {
            Result<IEnumerable<Class>?> result = await _Repo.GetAll();
            if (!result.IsSuccess|| result.Value == null || !result.Value.Any())
                return result.Error!;

            IEnumerable<ClassModel> r = result.Value.Select(x => _Mapper.Map<ClassModel>(x));
            return Result<IEnumerable<ClassModel>>.Success(r);; 
        }
    }
}