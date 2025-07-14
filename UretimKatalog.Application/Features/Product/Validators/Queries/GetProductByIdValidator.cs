using FluentValidation;
using Microsoft.Extensions.Localization;
using UretimKatalog.Application.Features.Product.Requests.Queries;

namespace UretimKatalog.Application.Features.Product.Validators.Queries
{
    public class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdValidator(
            IStringLocalizer<GetProductByIdValidator> localizer)
        {
            RuleFor(q => q.Id)
                .GreaterThan(0)
                .WithMessage(localizer["IdGreaterThanZero"]);   
        }
    }
}
