namespace UretimKatalog.Application.DTOs
{
    public class ProductImageDto
{
    public int    Id        { get; set; }
    public int    ProductId { get; set; }
    public string FileName  { get; set; } = null!;
    public string Url       { get; set; } = null!;
    public bool   IsMain    { get; set; }
}
}
