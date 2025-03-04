using Microsoft.AspNetCore.Mvc.Rendering;
using Rent_A_House_Amin_Mecha.Models;

namespace Rent_A_House_Amin_Mecha.ViewModels;

public class CreateOrderItemViewModel
{
    public OrderItem OrderItem { get; set; } = default!;
    public List<SelectListItem> ItemSelectList { get; set; } = default!;
    public List<SelectListItem> OrderSelectList { get; set; } = default!;
}


