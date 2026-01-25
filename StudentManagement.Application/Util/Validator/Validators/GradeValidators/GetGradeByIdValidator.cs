using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.Query.GradeQueryRequest;

namespace StudentManagement.Application.Util.Validator.Validators.GradeValidators
{
    public class GetGradeByIdValidator : AbstractValidator<GetGradeByIdRequest>
    {
        public GetGradeByIdValidator()
        {
            RuleFor(x => x.GradeId)
                .NotEmpty().WithMessage("معرف الدرجة مطلوب")
                .GreaterThan(0).WithMessage("معرف الدرجة يجب أن يكون أكبر من 0");
        }
    }
}