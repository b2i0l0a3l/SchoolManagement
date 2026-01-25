using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.StudentCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.StudentValidators
{
    public class UpdateStudentRequestValidator : AbstractValidator<UpdateStudentRequest>
    {
        public UpdateStudentRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("معرف الطالب مطلوب");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("الاسم الكامل مطلوب")
                .MaximumLength(100).WithMessage("الاسم لا يتجاوز 100 حرف")
                .MinimumLength(3).WithMessage("الاسم يجب أن يكون 3 أحرف على الأقل");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("تاريخ الميلاد مطلوب")
                .LessThan(DateTime.UtcNow).WithMessage("تاريخ الميلاد يجب أن يكون في الماضي");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("الجنس غير صالح");

            RuleFor(x => x.ClassId)
                .GreaterThan(0).WithMessage("معرف الصف مطلوب");
        }
    }
}
