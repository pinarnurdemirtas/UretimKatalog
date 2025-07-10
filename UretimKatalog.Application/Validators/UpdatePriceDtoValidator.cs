using FluentValidation;
using UretimKatalog.Application.DTOs;

namespace UretimKatalog.Application.Validators
{
    public class UpdatePriceDtoValidator : AbstractValidator<UpdatePriceDto>
    {
        public UpdatePriceDtoValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Fiyat negatif olamaz");
        }
    }
}