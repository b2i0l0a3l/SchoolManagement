 using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.TeacherSubjectCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.TeacherSubjectValidators
{
    public class UpdateTeacherSubjectRequestValidator : AbstractValidator<UpdateTeacherSubjectRequest>
    {
        public UpdateTeacherSubjectRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("معرف تعيين المعلم للمادة مطلوب");

            RuleFor(x => x.TeacherId)
                .GreaterThan(0).WithMessage("معرف المعلم مطلوب");

            RuleFor(x => x.ClassId)
                .GreaterThan(0).WithMessage("معرف الصف مطلوب");

            RuleFor(x => x.SubjectId)
                .GreaterThan(0).WithMessage("معرف المادة مطلوب");
        }
    }
}
