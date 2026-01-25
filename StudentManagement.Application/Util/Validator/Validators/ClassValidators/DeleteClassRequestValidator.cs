using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.ClassCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.ClassValidators
{
    public class DeleteClassRequestValidator : AbstractValidator<DeleteClassRequest>
    {
        public DeleteClassRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("معرف الصف مطلوب")
                .GreaterThan(0).WithMessage("معرف الصف يجب أن يكون أكبر من 0");
        }
    }
}