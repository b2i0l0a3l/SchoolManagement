using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.SubjectCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.SubjectValidators
{
    public class DeleteSubjectRequestValidator : AbstractValidator<DeleteSubjectRequest>
    {
        public DeleteSubjectRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("معرف المادة مطلوب")
                .GreaterThan(0).WithMessage("معرف المادة يجب أن يكون أكبر من 0");
        }
    }
}