using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.StudentCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.StudentValidators
{
    public class AddNewStudentRequestValidator : AbstractValidator<AddNewStudentRequest>
    {
        public AddNewStudentRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("معرف المستخدم مطلوب");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("الاسم الكامل مطلوب")
                .MaximumLength(100).WithMessage("الاسم لا يتجاوز 100 حرف")
                .MinimumLength(3).WithMessage("الاسم يجب أن يكون 3 أحرف على الأقل");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("تاريخ الميلاد مطلوب")
                .LessThan(DateTime.Now).WithMessage("تاريخ الميلاد يجب أن يكون في الماضي")
                .GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("تاريخ الميلاد غير صالح");

            RuleFor(x => x.EnrollmentDate)
                .NotEmpty().WithMessage("تاريخ التسجيل مطلوب")
                .LessThanOrEqualTo(DateTime.Now.AddDays(1)).WithMessage("تاريخ التسجيل غير صالح");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("الجنس غير صالح");

            RuleFor(x => x.ClassId)
                .GreaterThan(0).WithMessage("معرف الصف مطلوب");
        }
    }
}
