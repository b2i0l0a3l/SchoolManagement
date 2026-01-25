using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.TeacherCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.TeacherValidators
{
    public class AddNewTeacherRequestValidator : AbstractValidator<AddNewTeacherRequest>
    {
        public AddNewTeacherRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("معرف المستخدم مطلوب");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("الاسم الكامل مطلوب")
                .MaximumLength(100).WithMessage("الاسم لا يتجاوز 100 حرف")
                .MinimumLength(3).WithMessage("الاسم يجب أن يكون 3 أحرف على الأقل");

            RuleFor(x => x.HireDate)
                .NotEmpty().WithMessage("تاريخ التوظيف مطلوب")
                .LessThanOrEqualTo(DateTime.Now.AddDays(30)).WithMessage("تاريخ التوظيف غير صالح");

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("معرف القسم مطلوب");
        }
    }
}
