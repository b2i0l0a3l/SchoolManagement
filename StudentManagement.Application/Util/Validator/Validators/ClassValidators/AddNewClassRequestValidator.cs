using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.ClassCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.ClassValidators
{
    public class AddNewClassRequestValidator : AbstractValidator<AddNewClassRequest>
    {
        public AddNewClassRequestValidator()
        {
            RuleFor(x => x.ClassName)
                .NotEmpty().WithMessage("اسم الصف مطلوب")
                .MaximumLength(50).WithMessage("اسم الصف لا يتجاوز 50 حرف")
                .MinimumLength(2).WithMessage("اسم الصف يجب أن يكون حرفين على الأقل");

            RuleFor(x => x.Year)
                .NotEmpty().WithMessage("السنة الدراسية مطلوبة");
        }
    }
}
