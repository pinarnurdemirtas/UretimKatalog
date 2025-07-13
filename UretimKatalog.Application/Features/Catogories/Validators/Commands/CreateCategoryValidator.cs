using FluentValidation;
using Microsoft.Extensions.Localization;
using UretimKatalog.Application.Features.Categories.Requests.Commands;

namespace UretimKatalog.Application.Features.Categories.Validators.Commands
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator(IStringLocalizer<CreateCategoryValidator> L)
        {
            RuleFor(c => c.Dto.Name)
                .NotEmpty().WithMessage(L["NameNotEmpty"])
                .MaximumLength(100).WithMessage(L["NameMaxLen"]);
        }
    }
}