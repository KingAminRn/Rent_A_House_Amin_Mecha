﻿namespace Rent_A_House_Amin_Mecha.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    // navigation property
    public virtual List<Order>? Orders { get; set; }
}
