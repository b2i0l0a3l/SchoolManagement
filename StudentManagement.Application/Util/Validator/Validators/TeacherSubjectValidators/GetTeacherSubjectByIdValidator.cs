using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.Query.TeacherQueryRequest;
using StudentManagement.Application.Features.Messages.Request.Query.TeacherSubjectQueryRequest;

namespace StudentManagement.Application.Util.Validator.Validators.TeacherSubjectValidators
{
    public class GetTeacherSubjectByIdValidator : AbstractValidator<GetTeacherSubjectByIdRequest>
    {
        public GetTeacherSubjectByIdValidator()
        {
            RuleFor(x => x.TeacherSubjectId).NotEmpty().WithMessage("Id is Requried")
            .GreaterThan(0).WithMessage("Id must be greather than 0.");
        }
    }
}