using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.DepartmentCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.DepartmentValidators
{
    public class AddNewDepartmentRequestValidator : AbstractValidator<AddNewDepartmentRequest>
    {
        public AddNewDepartmentRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم القسم مطلوب")
                .MaximumLength(100).WithMessage("اسم القسم لا يتجاوز 100 حرف")
                .MinimumLength(2).WithMessage("اسم القسم يجب أن يكون حرفين على الأقل");
        }
    }
}
