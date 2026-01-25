using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.SubjectCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.SubjectValidators
{
    public class AddNewSubjectRequestValidator : AbstractValidator<AddNewSubjectRequest>
    {
        public AddNewSubjectRequestValidator()
        {
            RuleFor(x => x.SubjectName)
                .NotEmpty().WithMessage("اسم المادة مطلوب")
                .MaximumLength(100).WithMessage("اسم المادة لا يتجاوز 100 حرف")
                .MinimumLength(2).WithMessage("اسم المادة يجب أن يكون حرفين على الأقل");
        }
    }
}
