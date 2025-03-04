using System;
using Microsoft.EntityFrameworkCore;
using Rent_A_House_Amin_Mecha.Models;

namespace Rent_A_House_Amin_Mecha.DAL
{
    public class ItemDbContext : DbContext
    {
        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
        {
            Database.EnsureCreated();


        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}

/*Line6 defines that the class ItemDbContext inherits from DbContext*/
/*Line 6 – Line14 is the Constructor*/
/*Line10 creates an empty database in case it does not exist.*/
