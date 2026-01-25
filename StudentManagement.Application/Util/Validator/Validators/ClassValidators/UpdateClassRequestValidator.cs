using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.ClassCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.ClassValidators
{
    public class UpdateClassRequestValidator : AbstractValidator<UpdateClassRequest>
    {
        public UpdateClassRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("معرف الصف مطلوب");

            RuleFor(x => x.ClassName)
                .NotEmpty().WithMessage("اسم الصف مطلوب")
                .MaximumLength(50).WithMessage("اسم الصف لا يتجاوز 50 حرف")
                .MinimumLength(2).WithMessage("اسم الصف يجب أن يكون حرفين على الأقل");

            RuleFor(x => x.Year)
                .NotEmpty().WithMessage("السنة الدراسية مطلوبة");
        }
    }
}
