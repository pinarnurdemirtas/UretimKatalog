using FluentValidation;
using Microsoft.Extensions.Localization;
using UretimKatalog.Application.Features.Auth.Requests.Commands;

namespace UretimKatalog.Application.Features.Auth.Validators.Commands
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator(IStringLocalizer<LoginValidator> L)
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage(L["EmailNotEmpty"])         
                .EmailAddress().WithMessage(L["InvalidEmail"])
                .MaximumLength(255).WithMessage(L["EmailMaxLength"]);

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage(L["PasswordNotEmpty"])
                .MinimumLength(3).WithMessage(L["PasswordMinLength"])
                .Matches(@"^[^\s]+$").WithMessage(L["PasswordNoSpaces"])
                .MaximumLength(50).WithMessage(L["PasswordMaxLength"]);
        }
    }
}
