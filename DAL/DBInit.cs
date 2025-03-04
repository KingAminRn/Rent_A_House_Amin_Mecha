using Microsoft.EntityFrameworkCore;
using Rent_A_House_Amin_Mecha.Models;
using System;

namespace Rent_A_House_Amin_Mecha.DAL;

public static class DBInit
{
    public static void Seed(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        ItemDbContext context = serviceScope.ServiceProvider.GetRequiredService<ItemDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        if (!context.Items.Any())
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Magnificient Hotel",
                    Price = 150,
                    Description = "This hotel is right on the beach, offering beautiful ocean views. Guests can relax by the pool, enjoy fresh seafood, and explore nearby shops.",
                    ImageUrl = "/images/Hotel1.jpg"
                },
                new Item
                {
                    Name = "The All Star Hotel",
                    Price = 20,
                    Description = "A five-star hotel with spacious rooms, a spa, and fine dining. Perfect for a romantic getaway or a special occasion.",
                    ImageUrl = "/images/Hotel2.jpg"
                },
                new Item
                {
                    Name = "Madrid Specials",
                    Price = 50,
                    Description = "Located in an old building with traditional Spanish architecture, this hotel offers a unique and cultural experience.",
                    ImageUrl = "/images/Hotel3.jpg"
                },
                new Item
                {
                    Name = "Barcelona's Fancy Hotel",
                    Price = 250,
                    Description = "A cozy and affordable hotel with clean rooms and friendly staff. Ideal for travelers looking to explore without spending too much.",
                    ImageUrl = "/images/Hotel4.jpg"
                },
                new Item
                {
                    Name = "Barcelo Valencia Hotel",
                    Price = 150,
                    Description = "Right in the heart of the city, this hotel is close to museums, restaurants, and shopping areas. Great for sightseeing!",
                    ImageUrl = "/images/Hotel5.jpg"
                },
                new Item
                {
                    Name = "Hotel Giralda Hotel",
                    Price = 180,
                    Description = "A fun hotel with a kids’ pool, playground, and family-friendly activities. A great choice for vacations with children.",
                    ImageUrl = "/images/Hotel6.jpg"
                },
                new Item
                {
                    Name = "Hotel Casa Teva",
                    Price = 50,
                    Description = "Surrounded by nature, this hotel is perfect for hiking and relaxation. Guests can enjoy fresh air and stunning views.",
                    ImageUrl = "/images/Hotel7.jpg"
                },
                new Item
                {
                    Name = "Lamaro Hotel",
                    Price = 30,
                    Description = "A small and stylish hotel with personalized service, charming decor, and a peaceful atmosphere. Ideal for a relaxing stay.",
                    ImageUrl = "/images/Hotel8.jpg"
                },
            };
            context.AddRange(items);
            context.SaveChanges();
        }

        if (!context.Customers.Any())
        {
            var customers = new List<Customer>
            {
                new Customer { Name = "Alice Hansen", Address = "Osloveien 1"},
                new Customer { Name = "Bob Johansen", Address = "Oslomet gata 2"},
            };
            context.AddRange(customers);
            context.SaveChanges();
        }

        if (!context.Orders.Any())
        {
            var orders = new List<Order>
            {
                new Order {OrderDate = DateTime.Today.ToString(), CustomerId = 1,},
                new Order {OrderDate = DateTime.Today.ToString(), CustomerId = 2,},
            };
            context.AddRange(orders);
            context.SaveChanges();
        }

        //if (!context.OrderItems.Any())
        //{
        //    var orderItems = new List<OrderItem>
        //    {
        //        new OrderItem { ItemId = 1, Quantity = 2, OrderId = 1},
        //        new OrderItem { ItemId = 2, Quantity = 1, OrderId = 1},
        //        new OrderItem { ItemId = 3, Quantity = 4, OrderId = 2},
        //    };
        //    foreach (var orderItem in orderItems)
        //    {
        //        var item = context.Items.Find(orderItem.ItemId);
        //        orderItem.OrderItemPrice = orderItem.Quantity * item?.Price ?? 0;
        //    }
        //    context.AddRange(orderItems);
        //    context.SaveChanges();
        //}

        if (!context.OrderItems.Any())
        {
            var orderItems = new List<OrderItem>
    {
        new OrderItem { ItemId = 1, OrderFromDate = new DateTime(2023, 10, 7), OrderToDate = new DateTime(2023, 10, 10), OrderId = 1},
        new OrderItem { ItemId = 2, OrderFromDate = new DateTime(2023, 10, 7), OrderToDate = new DateTime(2023, 10, 12), OrderId = 1},
        new OrderItem { ItemId = 3, OrderFromDate = new DateTime(2023, 10, 7), OrderToDate = new DateTime(2023, 10, 9), OrderId = 2},
    };

            foreach (var orderItem in orderItems)
            {
                var item = context.Items.Find(orderItem.ItemId);
                int totalDays = (orderItem.OrderToDate - orderItem.OrderFromDate).Days;

                if (totalDays < 1) totalDays = 1; // Sikre minst 1 dag
                orderItem.TotalDays = totalDays; // Lagre verdien i databasen
                orderItem.OrderItemPrice = totalDays * (item?.Price ?? 0);
            }

            context.AddRange(orderItems);
            context.SaveChanges();
        }

       

        var ordersToUpdate = context.Orders.Include(o => o.OrderItems);
        foreach (var order in ordersToUpdate)
        {
            order.TotalPrice = order.OrderItems?.Sum(oi => oi.OrderItemPrice) ?? 0;
        }
        context.SaveChanges();
    }
}

