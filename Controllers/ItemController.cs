using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyModel;
using Rent_A_House_Amin_Mecha.Models;
using Rent_A_House_Amin_Mecha.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Rent_A_House_Amin_Mecha.DAL;

namespace Rent_A_House_Amin_Mecha.Controllers;

public class ItemController : Controller
{
    private readonly IItemRepository _itemRepository;

    public ItemController(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<IActionResult> Table()
    {
        var items = await _itemRepository.GetAll();
        var itemListViewModel = new ItemListViewModel(items, "Table");
        return View(itemListViewModel);
    }

    public async Task<IActionResult> Grid()
    {
        var items = await _itemRepository.GetAll();
        var itemListViewModel = new ItemListViewModel(items, "Grid");
        return View(itemListViewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var item = await _itemRepository.GetItemById(id);
        if (item == null)
            return BadRequest("Item not found.");
        return View(item);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Item item)
    {
        if (ModelState.IsValid)
        {
            await _itemRepository.Create(item);
            return RedirectToAction(nameof(Table));
        }

        return View(item);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var item = await _itemRepository.GetItemById(id);
        if (item == null)
        {
            return NotFound();
        }
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Item item)
    {
        if (ModelState.IsValid)
        {
            await _itemRepository.Update(item);
            return RedirectToAction(nameof(Table));
        }

        return View(item);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _itemRepository.GetItemById(id);
        if (item == null)
        {
            return NotFound();
        }
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _itemRepository.Delete(id);
        return RedirectToAction(nameof(Table));
    }




    //•	The ToList() function converts the results queried from the database into a List.
    /*•	/*You can notice that the GetItems() function that served as a mock database is no longer needed.
    •	Line13 – Line16 is the Constructor of the ItemController.It is called when an instance of ItemController is created, typically during the handling of an incoming HTTP request(when the Views controlled by ItemController are called, namely the Table, Grid, or Details).
    •	The ASP.NET Core framework handles the instantiation of the controller and its dependencies through dependency injection.*/




    //public IActionResult Table()
    //{
    //    var items = GetItems();
    //    ViewBag.CurrentViewName = "Table";
    //    return View(items);
    //}

    //public IActionResult Grid()
    //{
    //    var items = GetItems();
    //    ViewBag.CurrentViewName = "Grid";
    //    return View(items);
    //}

    public List<Item> GetItems()
    {
        var items = new List<Item>();
        var item1 = new Item
        {
            ItemId = 1,
            Name = "Pizza",
            Price = 150,
            Description = "Delicious Italian dish with a thin crust topped with tomato sauce, cheese, and various toppings.",
            ImageUrl = "/images/pizza.jpg"
        };

        var item2 = new Item
        {
            ItemId = 2,
            Name = "Fried Chicken Leg",
            Price = 20,
            Description = "Crispy and succulent chicken leg that is deep-fried to perfection, often served as a popular fast food item.",
            ImageUrl = "/images/chickenleg.jpg"
        };

        var item3 = new Item
        {
            ItemId = 3,
            Name = "French Fries",
            Price = 50,
            Description = "Crispy, golden-brown potato slices seasoned with salt and often served as a popular side dish or snack.",
            ImageUrl = "/images/frenchfries.jpg"
        };

        var item4 = new Item
        {
            ItemId = 4,
            Name = "Grilled Ribs",
            Price = 250,
            Description = "Tender and flavorful ribs grilled to perfection, usually served with barbecue sauce.",
            ImageUrl = "/images/ribs.jpg"
        };

        var item5 = new Item
        {
            ItemId = 5,
            Name = "Tacos",
            Price = 150,
            Description = "Tortillas filled with various ingredients such as seasoned meat, vegetables, and salsa, folded into a delicious handheld meal.",
            ImageUrl = "/images/tacos.jpg"
        };

        var item6 = new Item
        {
            ItemId = 6,
            Name = "Fish and Chips",
            Price = 180,
            Description = "Classic British dish featuring battered and deep-fried fish served with thick-cut fried potatoes.",
            ImageUrl = "/images/fishandchips.jpg"
        };

        var item7 = new Item
        {
            ItemId = 7,
            Name = "Cider",
            Price = 50,
            Description = "Refreshing alcoholic beverage made from fermented apple juice, available in various flavors.",
            ImageUrl = "/images/cider.jpg"
        };

        var item8 = new Item
        {
            ItemId = 8,
            Name = "Coke",
            Price = 30,
            Description = "Popular carbonated soft drink known for its sweet and refreshing taste.",
            ImageUrl = "/images/coke.jpg"
        };


        items.Add(item1);
        items.Add(item2);
        items.Add(item3);
        items.Add(item4);
        items.Add(item5);
        items.Add(item6);
        items.Add(item7);
        items.Add(item8);
        return items;
    }
}
