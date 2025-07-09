using System.Collections.Generic;

namespace UretimKatalog.Domain.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int? ParentCategoryId { get; set; }
        public Category? Parent { get; set; }
        public ICollection<Category> Children { get; set; } = new List<Category>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
