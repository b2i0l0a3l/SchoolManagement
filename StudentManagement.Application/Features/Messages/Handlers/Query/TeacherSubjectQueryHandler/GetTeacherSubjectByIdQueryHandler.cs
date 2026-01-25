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
    public class GetTeacherSubjectByIdQueryHandler : IRequestHandler<GetTeacherSubjectByIdRequest, Result<TeacherSubjectModel>>
    {
        private readonly IRepository<TeacherSubject> _Repo;
        private readonly IMapper _Mapper;
        public GetTeacherSubjectByIdQueryHandler(IRepository<TeacherSubject> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
        }
        public async Task<Result<TeacherSubjectModel>> Handle(GetTeacherSubjectByIdRequest request, CancellationToken cancellationToken)
        {
            Result<TeacherSubject?> result = await _Repo.GetById(request.TeacherSubjectId);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;
            return _Mapper.Map<TeacherSubjectModel>(result.Value);
        }
    }
}
