using System.ComponentModel.DataAnnotations;

namespace UretimKatalog.Domain.Models
{
    public class Review : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; } = string.Empty;
    }
}
