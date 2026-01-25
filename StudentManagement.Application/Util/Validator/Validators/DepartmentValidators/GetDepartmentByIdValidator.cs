using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.Query.DepartmentQueryRequest;

namespace StudentManagement.Application.Util.Validator.Validators.DepartmentValidators
{
    public class GetDepartmentByIdValidator : AbstractValidator<GetDepartmentByIdRequest>
    {
        public GetDepartmentByIdValidator()
        {
            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage("معرف القسم مطلوب")
                .GreaterThan(0).WithMessage("معرف القسم يجب أن يكون أكبر من 0");
        }
    }
}