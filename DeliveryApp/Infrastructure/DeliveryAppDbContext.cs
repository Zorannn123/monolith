using DeliveryApp.Enums;
using DeliveryApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DeliveryApp.Infrastructure
{
    public class DeliveryAppDbContext : DbContext
    {
       
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPart> OrderParts { get; set; }

        
        public DbSet<User> Users { get; set; }

        public DeliveryAppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeliveryAppDbContext).Assembly);
            modelBuilder.Entity<User>()
                .Property(u => u.Status)
                .HasConversion(
                    v => v.ToString(), 
                    v => (SupplierState)Enum.Parse(typeof(SupplierState), v)  
                );
        }
    }
}
