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
    public class GetClassStudentRequestHandler : IRequestHandler<GetClassStudentRequest, Result<ClassStudentModel>>
    {
        private readonly IRepository<Student> _StudentRepo;
        private readonly IMapper _mapper;
        public GetClassStudentRequestHandler(IRepository<Student> StudentRepo,IMapper mapper)
        {
            _mapper = mapper;
            _StudentRepo = StudentRepo;
        }
        public async Task<Result<ClassStudentModel>> Handle(GetClassStudentRequest request, CancellationToken cancellationToken)
        {
            Result<IEnumerable<Student>> Student = await _StudentRepo.GetAllByCondition(x => x.ClassId == request.ClassId);
            if (!Student.IsSuccess || Student.Value == null || !Student.Value.Any()) return Student.Error!;

            ClassStudentModel model = new()
            {
                ClassId = request.ClassId,
                Students = Student.Value.Select(x => _mapper.Map<StudentModel>(x)).ToList()
            };
           
            return model;
        }
    }
}