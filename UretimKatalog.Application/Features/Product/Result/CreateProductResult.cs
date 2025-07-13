namespace UretimKatalog.Application.Features.Product.Result
{
    public class CreateProductResult
{
    public int    Id          { get; set; }
    public string Name        { get; set; } = default!;
    public bool   IsActive    { get; set; }
    public decimal Price      { get; set; }
    public int    Stock       { get; set; }
    public int    CategoryId  { get; set; }
}

}
