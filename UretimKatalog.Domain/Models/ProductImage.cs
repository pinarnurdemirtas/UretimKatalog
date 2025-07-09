namespace UretimKatalog.Domain.Models
{
    public class ProductImage : BaseEntity
    {
        public string Url { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
