using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SystemInventory.Models;

namespace SystemInventoryWebNet5.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<UserApp> UserApp { get; set; }

        public DbSet<InventoryDetail> InventoryDetail { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<WarehouseProduct> WarehouseProduct { get; set; }
        public DbSet<Company> Company { get; set; }

        public DbSet<ShoppingCar> ShoppingCar { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
    }
}
