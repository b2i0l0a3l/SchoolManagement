using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.Query.StudentQueryRequest;

namespace StudentManagement.Application.Util.Validator.Validators.StudentValidators
{
    public class GetStudentByIdValidator : AbstractValidator<GetStudentByIdRequest>
    {
        public GetStudentByIdValidator()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("معرف الطالب مطلوب")
                .GreaterThan(0).WithMessage("معرف الطالب يجب أن يكون أكبر من 0");
        }
    }
}