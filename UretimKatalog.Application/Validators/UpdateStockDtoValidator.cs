using FluentValidation;
using UretimKatalog.Application.DTOs;

namespace UretimKatalog.Application.Validators
{
    public class UpdateStockDtoValidator : AbstractValidator<UpdateStockDto>
    {
        public UpdateStockDtoValidator()
        {
            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stok negatif olamaz");
        }
    }
}