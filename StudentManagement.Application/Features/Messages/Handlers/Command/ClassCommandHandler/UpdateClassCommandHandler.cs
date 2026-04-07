using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using StudentManagement.Application.Features.Messages.Request.command.ClassCommandRequest;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;


namespace StudentManagement.Application.Features.Messages.Handlers.Command.ClassCommandHandler
{
    public class UpdateClassCommandHandler : IRequestHandler<UpdateClassRequest, Result<bool>>
    {
        private readonly IRepository<Class> _Repo;
        public UpdateClassCommandHandler(IRepository<Class> Repo)
        {
            _Repo = Repo;

        }
        public async Task<Result<bool>> Handle(UpdateClassRequest request, CancellationToken cancellationToken)
        {
            var entityResult = await _Repo.GetById(request.Id);
            if (!entityResult.IsSuccess || entityResult.Value == null)
                return entityResult.IsSuccess ? Errors.UserNotFoundError : entityResult.Error!;



            Result<bool> res = await _Repo.Update(request.Id, x =>
            {
                x.ClassName = request.ClassName;
                x.Year = request.Year;
            
            });

            if (!res.IsSuccess)
                return res.Error!;
            return res.Value;
        }
    }
}