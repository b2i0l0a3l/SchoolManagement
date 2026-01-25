using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.TeacherSubjectQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.TeacherSubjectQueryHandler
{
    public class GetAllTeacherSubjectsQueryHandler : IRequestHandler<GetAllTeacherSubjectsRequest, Result<IEnumerable<TeacherSubjectModel>>>
    {
        private readonly IRepository<TeacherSubject> _Repo;
        private readonly IMapper _Mapper;
        public GetAllTeacherSubjectsQueryHandler(IRepository<TeacherSubject> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
        }
        public async Task<Result<IEnumerable<TeacherSubjectModel>>> Handle(GetAllTeacherSubjectsRequest request, CancellationToken cancellationToken)
        {
            Result<IEnumerable<TeacherSubject>?> result = await _Repo.GetAll();
            if (!result.IsSuccess || result.Value == null || !result.Value.Any())
                return result.Error!;
            IEnumerable<TeacherSubjectModel> r = result.Value.Select(x => _Mapper.Map<TeacherSubjectModel>(x));
            return Result<IEnumerable<TeacherSubjectModel>>.Success(r);
        }
    }
}
