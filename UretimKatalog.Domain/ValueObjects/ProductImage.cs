// ValueObjects/ProductImage.cs
namespace UretimKatalog.Domain.ValueObjects
{
    public class ProductImage
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public string FileName { get; private set; }
        public string Url { get; private set; }

        public ProductImage(Guid productId, string fileName, string url)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            FileName = fileName;
            Url = url;
        }
    }
}
