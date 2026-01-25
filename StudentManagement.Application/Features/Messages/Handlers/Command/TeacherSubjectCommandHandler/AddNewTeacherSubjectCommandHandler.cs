using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.TeacherSubjectCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.TeacherSubjectCommandHandler
{
    public class AddNewTeacherSubjectCommandHandler : IRequestHandler<AddNewTeacherSubjectRequest, Result<TeacherSubjectModel>>
    {
        private readonly IRepository<TeacherSubject> _Repo;
        private IMapper _Mapper;
        public AddNewTeacherSubjectCommandHandler(IRepository<TeacherSubject> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
        }
        public async Task<Result<TeacherSubjectModel>> Handle(AddNewTeacherSubjectRequest request, CancellationToken cancellationToken)
        {
            
            Result<TeacherSubject> result = await _Repo.Add(_Mapper.Map<TeacherSubject>(request));
            if (!result.IsSuccess)
                return result.Error!;
            return _Mapper.Map<TeacherSubjectModel>(result.Value);
        }
    }
}
