﻿using Rent_A_House_Amin_Mecha.Models;
using System.ComponentModel.DataAnnotations;

namespace Rent_A_House_Amin_Mecha.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        // navigation property
        public virtual List<OrderItem>? OrderItems { get; set; }
    }
}

