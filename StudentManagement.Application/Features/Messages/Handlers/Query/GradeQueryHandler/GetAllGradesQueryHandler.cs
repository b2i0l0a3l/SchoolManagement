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
    public class GetAllGradesQueryHandler : IRequestHandler<GetAllGradesRequest, Result<IEnumerable<GradeModel>>>
    {
        private readonly IRepository<Grade> _Repo;
        private readonly IMapper _Mapper;
        public GetAllGradesQueryHandler(IRepository<Grade> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
        }
        public async Task<Result<IEnumerable<GradeModel>>> Handle(GetAllGradesRequest request, CancellationToken cancellationToken)
        {
            Result<IEnumerable<Grade>?> result = await _Repo.GetAll();
            if (!result.IsSuccess || result.Value == null || !result.Value.Any())
                return result.Error!;
            IEnumerable<GradeModel> r = result.Value.Select(x => _Mapper.Map<GradeModel>(x));
            return Result<IEnumerable<GradeModel>>.Success(r);
        }
    }
}
