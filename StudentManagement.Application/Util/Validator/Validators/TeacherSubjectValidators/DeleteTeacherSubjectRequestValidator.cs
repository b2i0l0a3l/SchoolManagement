using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.TeacherSubjectCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.TeacherSubjectValidators
{
    public class DeleteTeacherSubjectRequestValidator : AbstractValidator<DeleteTeacherSubjectRequest>
    {
        public DeleteTeacherSubjectRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("معرف المعلم والمادة مطلوب")
                .GreaterThan(0).WithMessage("معرف المعلم والمادة يجب أن يكون أكبر من 0");
        }
    }
}