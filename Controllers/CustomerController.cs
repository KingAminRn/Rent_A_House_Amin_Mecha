using Microsoft.AspNetCore.Mvc;
using Rent_A_House_Amin_Mecha.Models;
using Microsoft.EntityFrameworkCore;
using Rent_A_House_Amin_Mecha.DAL;


namespace Rent_A_House_Amin_Mecha.Controllers;

public class CustomerController : Controller
{
    private readonly ItemDbContext _itemDbContext;

    public CustomerController(ItemDbContext itemDbContext)
    {
        _itemDbContext = itemDbContext;
    }

    public async Task<IActionResult> Table()
    {
        List<Customer> customers = await _itemDbContext.Customers.ToListAsync();
        return View(customers);
    }
}
