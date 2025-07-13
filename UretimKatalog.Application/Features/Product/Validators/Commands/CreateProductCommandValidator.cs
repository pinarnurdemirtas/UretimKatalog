using FluentValidation;
using Microsoft.Extensions.Localization;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Application.Features.Product.Requests.Commands;

namespace UretimKatalog.Application.Features.Product.Validators.Commands
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator(
            ICategoryRepository repo,
            IStringLocalizer<CreateProductCommandValidator> localizer)
        {
            // Kategori kontrolü
            RuleFor(c => c.Dto.CategoryId)
                .GreaterThan(0).WithMessage(localizer["CategoryIdGreaterThanZero"])
                .MustAsync(async (_, id, ct) => await repo.ExistsAsync(id))
                .WithMessage(localizer["CategoryNotFound"]);

            // Ürün adı
            RuleFor(c => c.Dto.Name)
                .NotEmpty().WithMessage(localizer["NameNotEmpty"])
                .MaximumLength(200).WithMessage(localizer["NameMaxLength"]);

            // Fiyat
            RuleFor(c => c.Dto.Price)
                .GreaterThan(0).WithMessage(localizer["PriceGreaterThanZero"]);

            // Stok
            RuleFor(c => c.Dto.Stock)
                .GreaterThanOrEqualTo(0).WithMessage(localizer["StockNotNegative"]);
        }
    }
}
