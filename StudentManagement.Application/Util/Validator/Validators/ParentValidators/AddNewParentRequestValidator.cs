using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.ParentCommandRequest;

namespace StudentManagement.Application.Util.Validator.Validators.ParentValidators
{
    public class AddNewParentRequestValidator : AbstractValidator<AddNewParentRequest>
    {
        public AddNewParentRequestValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("اسم ولي الأمر مطلوب")
                .MaximumLength(40).WithMessage("الاسم لا يمكن أن يتجاوز 40 حرف");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("رقم الهاتف مطلوب");
        }
    }
}
