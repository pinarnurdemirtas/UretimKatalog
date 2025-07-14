using System.Collections.Generic;

namespace UretimKatalog.Domain.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        

        
    }
}
