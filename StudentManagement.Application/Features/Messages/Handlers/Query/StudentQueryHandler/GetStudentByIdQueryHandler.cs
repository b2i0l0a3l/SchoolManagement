using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.StudentQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.StudentQueryHandler
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdRequest, Result<StudentModel>>
    {
        private readonly IRepository<Student> _Repo;
        private readonly IMapper _Mapper;
        public GetStudentByIdQueryHandler(IRepository<Student> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
        }
        public async Task<Result<StudentModel>> Handle(GetStudentByIdRequest request, CancellationToken cancellationToken)
        {
            Result<Student?> result = await _Repo.GetById(request.StudentId);
            if (!result.IsSuccess || result.Value == null)
                return result.Error!;
            return _Mapper.Map<StudentModel>(result.Value);
        }
    }
}
