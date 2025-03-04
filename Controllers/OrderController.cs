using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rent_A_House_Amin_Mecha.Models;
using Rent_A_House_Amin_Mecha.ViewModels;
using Rent_A_House_Amin_Mecha.DAL;
using System;

namespace Rent_A_House_Amin_Mecha.Controllers;

public class OrderController : Controller
{
    private readonly ItemDbContext _itemDbContext;

    public OrderController(ItemDbContext itemDbContext)
    {
        _itemDbContext = itemDbContext;
    }

    public async Task<IActionResult> Table()
    {
        List<Order> orders = await _itemDbContext.Orders.ToListAsync();
        return View(orders);
    }

    //[HttpGet]
    //public async Task<IActionResult> CreateOrderItem()
    //{
    //    var items = await _itemDbContext.Items.ToListAsync();
    //    var orders = await _itemDbContext.Orders.ToListAsync();
    //    var createOrderItemViewModel = new CreateOrderItemViewModel
    //    {
    //        OrderItem = new OrderItem(),

    //        ItemSelectList = items.Select(item => new SelectListItem
    //        {
    //            Value = item.ItemId.ToString(),
    //            Text = item.ItemId.ToString() + ": " + item.Name
    //        }).ToList(),

    //        OrderSelectList = orders.Select(order => new SelectListItem
    //        {
    //            Value = order.OrderId.ToString(),
    //            Text = "Order" + order.OrderId.ToString() + ", Date: " + order.OrderDate + ", Customer: " + order.Customer.Name
    //        }).ToList(),
    //    };
    //    return View(createOrderItemViewModel);
    //}

    //[HttpPost]
    //public async Task<IActionResult> CreateOrderItem(OrderItem orderItem)
    //{
    //    try
    //    {
    //        var newItem = _itemDbContext.Items.Find(orderItem.ItemId);
    //        var newOrder = _itemDbContext.Orders.Find(orderItem.OrderId);

    //        if (newItem == null || newOrder == null)
    //        {
    //            return BadRequest("Item or Order not found.");
    //        }

    //        var newOrderItem = new OrderItem
    //        {
    //            ItemId = orderItem.ItemId,
    //            Item = newItem,
    //            Quantity = orderItem.Quantity,
    //            OrderId = orderItem.OrderId,
    //            Order = newOrder,
    //        };
    //        newOrderItem.OrderItemPrice = orderItem.Quantity * newOrderItem.Item.Price;

    //        _itemDbContext.OrderItems.Add(newOrderItem);
    //        await _itemDbContext.SaveChangesAsync();
    //        return RedirectToAction(nameof(Table));
    //    }
    //    catch
    //    {
    //        return BadRequest("OrderItem creation failed.");
    //    }
    //}
    //[HttpPost]
    //public async Task<IActionResult> CreateOrderFromDetails(int ItemId, int Quantity)
    //{
    //    var item = await _itemDbContext.Items.FindAsync(ItemId);
    //    if (item == null)
    //    {
    //        return NotFound("Item not found.");
    //    }

    //    // Finn en eksisterende kunde (her kan det tilpasses for innloggede brukere senere)
    //    var customer = await _itemDbContext.Customers.FirstOrDefaultAsync();
    //    if (customer == null)
    //    {
    //        return BadRequest("No customer found. Please create a customer first.");
    //    }

    //    // Opprett en ny ordre
    //    var order = new Order
    //    {
    //        OrderDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
    //        CustomerId = customer.CustomerId,
    //        Customer = customer,
    //        TotalPrice = Quantity * item.Price
    //    };
    //    _itemDbContext.Orders.Add(order);
    //    await _itemDbContext.SaveChangesAsync(); // Må lagres for å få OrderId

    //    // Opprette et nytt OrderItem i den nye ordren
    //    var orderItem = new OrderItem
    //    {
    //        ItemId = ItemId,
    //        Item = item,
    //        Quantity = Quantity,
    //        OrderId = order.OrderId,
    //        Order = order,
    //        OrderItemPrice = Quantity * item.Price
    //    };

    //    _itemDbContext.OrderItems.Add(orderItem);
    //    await _itemDbContext.SaveChangesAsync();

    //    return RedirectToAction("Table", "Order"); // Tilbake til oversikten
    //}

    //[HttpPost]
    //public async Task<IActionResult> CreateOrderFromDetails(int ItemId, DateTime OrderFromDate, DateTime OrderToDate)
    //{
    //    var item = await _itemDbContext.Items.FindAsync(ItemId);
    //    if (item == null)
    //    {
    //        return NotFound("Item not found.");
    //    }

    //    // Finn en eksisterende kunde (dersom flere finnes, kan en utvelgelseslogikk implementeres)
    //    var customer = await _itemDbContext.Customers.FirstOrDefaultAsync();
    //    if (customer == null)
    //    {
    //        return BadRequest("No customer found. Please create a customer first.");
    //    }

    //    // Opprett en ny ordre
    //    var order = new Order
    //    {
    //        OrderDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
    //        CustomerId = customer.CustomerId,
    //        Customer = customer,
    //        TotalPrice = 0 // Blir oppdatert senere
    //    };
    //    _itemDbContext.Orders.Add(order);
    //    await _itemDbContext.SaveChangesAsync(); // Må lagres for å få OrderId

    //    // Beregn antall dager
    //    int totalDays = (OrderToDate - OrderFromDate).Days;
    //    if (totalDays < 1)
    //    {
    //        return BadRequest("Order period must be at least 1 day.");
    //    }

    //    // Opprette et nytt OrderItem med bestillingsdatoer
    //    var orderItem = new OrderItem
    //    {
    //        ItemId = ItemId,
    //        Item = item,
    //        OrderId = order.OrderId,
    //        Order = order,
    //        OrderFromDate = OrderFromDate,
    //        OrderToDate = OrderToDate,
    //        OrderItemPrice = totalDays * item.Price // Pris basert på antall dager
    //    };

    //    _itemDbContext.OrderItems.Add(orderItem);
    //    await _itemDbContext.SaveChangesAsync();

    //    // Oppdatere totalprisen for ordren
    //    order.TotalPrice = orderItem.OrderItemPrice;
    //    _itemDbContext.Orders.Update(order);
    //    await _itemDbContext.SaveChangesAsync();

    //    return RedirectToAction("Table", "Item"); // Tilbake til oversikten
    //}

    [HttpPost]
    public async Task<IActionResult> CreateOrderFromDetails(int ItemId, DateTime OrderFromDate, DateTime OrderToDate)
    {
        var item = await _itemDbContext.Items.FindAsync(ItemId);
        if (item == null)
        {
            return NotFound("Item not found.");
        }

        // Finn en eksisterende kunde
        var customer = await _itemDbContext.Customers.FirstOrDefaultAsync();
        if (customer == null)
        {
            return BadRequest("No customer found. Please create a customer first.");
        }

        // Opprett en ny ordre
        var order = new Order
        {
            OrderDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            CustomerId = customer.CustomerId,
            Customer = customer,
            TotalPrice = 0 // Oppdateres senere
        };
        _itemDbContext.Orders.Add(order);
        await _itemDbContext.SaveChangesAsync(); // Må lagres for å få OrderId

        // Beregn antall dager
        int totalDays = (OrderToDate - OrderFromDate).Days;
        if (totalDays < 1) totalDays = 1; // Sikre minst 1 dag

        // Opprette et nytt OrderItem med bestillingsdatoer
        var orderItem = new OrderItem
        {
            ItemId = ItemId,
            Item = item,
            OrderId = order.OrderId,
            Order = order,
            OrderFromDate = OrderFromDate,
            OrderToDate = OrderToDate,
            TotalDays = totalDays, // Nytt felt
            OrderItemPrice = totalDays * item.Price
        };

        _itemDbContext.OrderItems.Add(orderItem);
        await _itemDbContext.SaveChangesAsync();

        // Oppdatere totalprisen for ordren
        order.TotalPrice = orderItem.OrderItemPrice;
        _itemDbContext.Orders.Update(order);
        await _itemDbContext.SaveChangesAsync();

        return RedirectToAction("Table", "Order");
    }

}
