using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.TeacherQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.TeacherQueryHandler
{
    public class GetTeacherByIdQueryHandler : IRequestHandler<GetTeacherByIdRequest, Result<TeacherModel>>
    {
        private readonly IRepository<Teacher> _Repo;
        private readonly IMapper _Mapper;
        public GetTeacherByIdQueryHandler(IRepository<Teacher> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
        }
        public async Task<Result<TeacherModel>> Handle(GetTeacherByIdRequest request, CancellationToken cancellationToken)
        {
            Result<Teacher?> result = await _Repo.GetById(request.TeacherId);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;
            return _Mapper.Map<TeacherModel>(result.Value);
        }
    }
}
