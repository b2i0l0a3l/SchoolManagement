using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.TeacherCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.TeacherValidators
{
    public class UpdateTeacherRequestValidator : AbstractValidator<UpdateTeacherRequest>
    {
        public UpdateTeacherRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("معرف المعلم مطلوب");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("الاسم الكامل مطلوب")
                .MaximumLength(100).WithMessage("الاسم لا يتجاوز 100 حرف")
                .MinimumLength(3).WithMessage("الاسم يجب أن يكون 3 أحرف على الأقل");

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("معرف القسم مطلوب");
        }
    }
}
