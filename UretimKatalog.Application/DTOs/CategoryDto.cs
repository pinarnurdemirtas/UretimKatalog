namespace UretimKatalog.Application.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? ParentCategoryId { get; set; }
    }
}
