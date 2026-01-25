using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.Query.TeacherQueryRequest;

namespace StudentManagement.Application.Util.Validator.Validators.TeacherValidators
{
    public class GetTeacherByIdValidator : AbstractValidator<GetTeacherByIdRequest>
    {
        public GetTeacherByIdValidator()
        {
            RuleFor(x => x.TeacherId)
                .NotEmpty().WithMessage("معرف المعلم مطلوب")
                .GreaterThan(0).WithMessage("معرف المعلم يجب أن يكون أكبر من 0");
        }
    }
}