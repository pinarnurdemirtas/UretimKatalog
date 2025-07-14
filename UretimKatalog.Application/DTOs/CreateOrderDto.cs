using System;
using System.Collections.Generic;
using UretimKatalog.Application.DTOs;

namespace UretimKatalog.Application.DTOs
{
    public class CreateOrderDto
    {
        public decimal TotalAmount  { get; set; }
        public int UserId           { get; set; }
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
