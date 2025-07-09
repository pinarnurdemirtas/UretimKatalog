using FluentValidation;
using UretimKatalog.Application.DTOs;

namespace UretimKatalog.Application.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Price).GreaterThan(0);
            // daha fazla kural...
        }
    }
}
