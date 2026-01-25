using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.Query.StudentQueryRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Query.StudentQueryHandler
{
    public class GetPassedStudentByClassIdRequestHandler : IRequestHandler<GetPassedStudentByClassIdQueryRequest, Result<IEnumerable<StudentGradeModel>>>
    {
        private readonly IStudentRepo _studentRepo;
        public GetPassedStudentByClassIdRequestHandler(IStudentRepo studentRepo)
        {
            _studentRepo = studentRepo;
        }
        public async Task<Result<IEnumerable<StudentGradeModel>>> Handle(GetPassedStudentByClassIdQueryRequest request, CancellationToken cancellationToken)
        {
            Result<List<StudentGradeModel>> res = await _studentRepo.GetPassedStudentByClassId(request.ClassId);
            if (!res.IsSuccess || res.Value == null) return res.Error!;        
            return res.Value;
        }
    }
}