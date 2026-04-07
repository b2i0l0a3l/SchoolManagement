using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.GradeCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.GradeValidators
{
    public class UpdateGradeRequestValidator : AbstractValidator<UpdateGradeRequest>
    {
        public UpdateGradeRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("معرف الدرجة مطلوب");

            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("معرف الطالب مطلوب");

            RuleFor(x => x.ExamId)
                .GreaterThan(0).WithMessage("معرف الامتحان مطلوب");

            RuleFor(x => x.Score)
                .InclusiveBetween(0, 100).WithMessage("الدرجة يجب أن تكون بين 0 و 100");
        }
    }
}
