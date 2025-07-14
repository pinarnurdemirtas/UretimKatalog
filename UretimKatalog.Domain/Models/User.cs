using System;

namespace UretimKatalog.Domain.Models
{
    public class User : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
