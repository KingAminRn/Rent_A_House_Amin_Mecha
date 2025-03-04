using Rent_A_House_Amin_Mecha.Models;
using System.ComponentModel.DataAnnotations;

namespace Rent_A_House_Amin_Mecha.Models;

//public class OrderItem
//{
//    public int OrderItemId { get; set; }
//    public int ItemId { get; set; }
//    //navigation property
//    public virtual Item Item { get; set; } = default!;
//    public int Quantity { get; set; }
//    public int OrderId { get; set; }
//    //navigation property
//    public virtual Order Order { get; set; } = default!;
//    public decimal OrderItemPrice { get; set; }
//}
public class OrderItem
{
    public int OrderItemId { get; set; }
    public int ItemId { get; set; }
    // Navigasjonsfelt
    public virtual Item Item { get; set; } = default!;
    public int OrderId { get; set; }
    public virtual Order Order { get; set; } = default!;

    // Nytt: Bestillingsperiode
    [DataType(DataType.Date)]
    public DateTime OrderFromDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime OrderToDate { get; set; }

    // Nytt: Antall dager
    public int TotalDays { get; set; }

    public decimal OrderItemPrice { get; set; }
}
