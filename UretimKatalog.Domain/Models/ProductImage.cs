using System;

namespace UretimKatalog.Domain.Models
{
    public class ProductImage : BaseEntity
    {
        // EF Core için parameterless ctor
        private ProductImage() { }

        // Dışarıdan çağrılacak public ctor
        public ProductImage(int productId, string fileName, string url, bool isMain)
        {
            ProductId = productId;
            FileName  = fileName;
            Url       = url;
            IsMain    = isMain;
        }

        public int ProductId { get; private set; }
        public Product Product { get; private set; }

        public string FileName { get; private set; }
        public string Url      { get; private set; }
        public bool   IsMain   { get; private set; }
    }
}
