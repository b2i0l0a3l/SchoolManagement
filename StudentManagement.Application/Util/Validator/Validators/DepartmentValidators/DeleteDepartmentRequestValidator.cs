using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.DepartmentCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.DepartmentValidators
{
    public class DeleteDepartmentRequestValidator : AbstractValidator<DeleteDepartmentRequest>
    {
        public DeleteDepartmentRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("معرف القسم مطلوب")
                .GreaterThan(0).WithMessage("معرف القسم يجب أن يكون أكبر من 0");
        }
    }
}