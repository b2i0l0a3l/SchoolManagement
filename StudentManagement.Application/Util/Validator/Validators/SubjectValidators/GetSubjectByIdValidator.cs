using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.Query.SubjectQueryRequest;

namespace StudentManagement.Application.Util.Validator.Validators.SubjectValidators
{
    public class GetSubjectByIdValidator : AbstractValidator<GetSubjectByIdRequest>
    {
        public GetSubjectByIdValidator()
        {
            RuleFor(x => x.SubjectId)
                .NotEmpty().WithMessage("معرف المادة مطلوب")
                .GreaterThan(0).WithMessage("معرف المادة يجب أن يكون أكبر من 0");
        }
    }
}