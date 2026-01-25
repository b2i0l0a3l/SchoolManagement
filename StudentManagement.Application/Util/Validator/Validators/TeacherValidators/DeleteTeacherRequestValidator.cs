using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.TeacherCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.TeacherValidators
{
    public class DeleteTeacherRequestValidator : AbstractValidator<DeleteTeacherRequest>
    {
        public DeleteTeacherRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("معرف المعلم مطلوب")
                .GreaterThan(0).WithMessage("معرف المعلم يجب أن يكون أكبر من 0");
        }
    }
}