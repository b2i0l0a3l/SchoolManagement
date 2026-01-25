using FluentValidation;
using StudentManagement.Application.Features.Messages.Request.command.LoginRequest;

namespace StudentManagement.Application.Validators.AuthValidators
{
    public class LoginRequestValidator : AbstractValidator<LoginCommandRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("البريد الإلكتروني مطلوب")
                .EmailAddress().WithMessage("صيغة البريد الإلكتروني غير صحيحة");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("كلمة المرور مطلوبة");
        }
    }
}
