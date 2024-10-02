using DeliveryApp.Enums;
using DeliveryApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DeliveryApp.Infrastructure
{
    public class DeliveryAppDbContext : DbContext
    {
        // Products-related tables
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPart> OrderParts { get; set; }

        // Users-related tables
        public DbSet<User> Users { get; set; }

        public DeliveryAppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations from all assemblies if needed
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeliveryAppDbContext).Assembly);
            modelBuilder.Entity<User>()
                .Property(u => u.Status)
                .HasConversion(
                    v => v.ToString(),  // Convert enum to string when saving to the database
                    v => (SupplierState)Enum.Parse(typeof(SupplierState), v)  // Convert string back to enum when loading from the database
                );
        }
    }
}
