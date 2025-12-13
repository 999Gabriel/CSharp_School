using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OrmExercises.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        
        // Navigation Property für 1:n (Category -> Products)
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        // Navigation Property für m:n (Product <-> Order)
        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        
        // Navigation Property
        public List<Product> Products { get; set; } = new List<Product>();
    }

    public class Order
    {
        public int OrderId { get; set; }
        public System.DateTime OrderDate { get; set; }
        
        // Navigation Property für m:n
        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }

    // Zwischentabelle für m:n Beziehung
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        public int Quantity { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=localhost;user=root;password=Nij43Bq8;database=orm_exercises";
            var serverVersion = new MySqlServerVersion(new System.Version(8, 0, 29));
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguration des zusammengesetzten Primärschlüssels für die Zwischentabelle
            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductId);
        }
    }
}

