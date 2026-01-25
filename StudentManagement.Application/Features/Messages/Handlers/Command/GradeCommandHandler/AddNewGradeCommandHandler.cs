using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.GradeCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Handlers.Command.GradeCommandHandler
{
    public class AddNewGradeCommandHandler : IRequestHandler<AddNewGradeRequest, Result<GradeModel>>
    {
        private readonly IRepository<Grade> _Repo;
        private IMapper _Mapper;
        public AddNewGradeCommandHandler(IRepository<Grade> Repo, IMapper Mapper)
        {
            _Repo = Repo;
            _Mapper = Mapper;
        }
        public async Task<Result<GradeModel>> Handle(AddNewGradeRequest request, CancellationToken cancellationToken)
        {
            Result<Grade> result = await _Repo.Add(_Mapper.Map<Grade>(request));
            if (!result.IsSuccess)
                return result.Error!;
            return _Mapper.Map<GradeModel>(result.Value);
        }
    }
}
