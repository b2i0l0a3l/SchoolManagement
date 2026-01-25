using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.GradeQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.GradeQueryHandler
{
    public class GetGradeByIdQueryHandler : IRequestHandler<GetGradeByIdRequest, Result<GradeModel>>
    {
        private readonly IRepository<Grade> _Repo;
        private readonly IMapper _Mapper;
        public GetGradeByIdQueryHandler(IRepository<Grade> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
        }
        public async Task<Result<GradeModel>> Handle(GetGradeByIdRequest request, CancellationToken cancellationToken)
        {
            Result<Grade?> result = await _Repo.GetById(request.GradeId);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;
            return _Mapper.Map<GradeModel>(result.Value);
        }
    }
}
