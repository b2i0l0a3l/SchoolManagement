using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.ExamCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.ExamValidators
{
    public class UpdateExamRequestValidator : AbstractValidator<UpdateExamRequest>
    {
        public UpdateExamRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("معرف الامتحان مطلوب");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("عنوان الامتحان مطلوب")
                .MaximumLength(100).WithMessage("العنوان لا يمكن أن يتجاوز 100 حرف");

            RuleFor(x => x.SubjectId)
                .GreaterThan(0).WithMessage("معرف المادة مطلوب");

            RuleFor(x => x.MaxScore)
                .GreaterThan(0).WithMessage("الدرجة القصوى يجب أن تكون أكبر من 0");
        }
    }
}
