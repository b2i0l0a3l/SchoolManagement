using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.Query.ClassQueryRequest;

namespace StudentManagement.Application.Util.Validator.Validators.ClassValidators
{
    public class GetClassByIdValidator : AbstractValidator<GetClassByIdRequest>
    {
        public GetClassByIdValidator()
        {
            RuleFor(x => x.ClassId)
                .NotEmpty().WithMessage("معرف الصف مطلوب")
                .GreaterThan(0).WithMessage("معرف الصف يجب أن يكون أكبر من 0");
        }
    }
}