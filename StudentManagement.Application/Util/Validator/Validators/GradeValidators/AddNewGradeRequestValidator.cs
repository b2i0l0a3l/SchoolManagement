using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.GradeCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.GradeValidators
{
    public class AddNewGradeRequestValidator : AbstractValidator<AddNewGradeRequest>
    {
        public AddNewGradeRequestValidator()
        {
            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("معرف الطالب مطلوب");

            RuleFor(x => x.ExamId)
                .GreaterThan(0).WithMessage("معرف الامتحان مطلوب");

            RuleFor(x => x.Score)
                .InclusiveBetween(0, 100).WithMessage("الدرجة يجب أن تكون بين 0 و 100");
        }
    }
}
