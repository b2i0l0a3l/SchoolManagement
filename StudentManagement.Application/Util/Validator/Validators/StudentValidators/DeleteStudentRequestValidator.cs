using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.StudentCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.StudentValidators
{
    public class DeleteStudentRequestValidator : AbstractValidator<DeleteStudentRequest>
    {
        public DeleteStudentRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("معرف الطالب مطلوب")
                .GreaterThan(0).WithMessage("معرف الطالب يجب أن يكون أكبر من 0");
        }
    }
}