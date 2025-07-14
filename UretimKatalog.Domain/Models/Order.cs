using System;
using System.Collections.Generic;
using UretimKatalog.Domain.Models;

namespace UretimKatalog.Domain.Models
{
    public class Order : BaseEntity
    {
        public decimal TotalAmount { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
