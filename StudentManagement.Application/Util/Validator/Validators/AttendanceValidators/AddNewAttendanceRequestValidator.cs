using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.AttendanceCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.AttendanceValidators
{
    public class AddNewAttendanceRequestValidator : AbstractValidator<AddNewAttendanceRequest>
    {
        public AddNewAttendanceRequestValidator()
        {
            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("معرف الطالب مطلوب");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("حالة الحضور غير صحيحة");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("التاريخ مطلوب");
        }
    }
}
