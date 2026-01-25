using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.GradeCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.GradeValidators
{
    public class DeleteGradeRequestValidator : AbstractValidator<DeleteGradeRequest>
    {
        public DeleteGradeRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("معرف الدرجة مطلوب")
                .GreaterThan(0).WithMessage("معرف الدرجة يجب أن يكون أكبر من 0");
        }
    }
}