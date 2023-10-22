using Microsoft.EntityFrameworkCore;

namespace PatatzaakOfficeel.Models
{
    public class PatatzaakDBContext : DbContext
    {
        
        
           
            public DbSet<Admin> Admins { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Customer> Customers { get; set; }
            public DbSet<Discount> Discounts { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<Orderitem> Orderitems { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<Cart> Carts { get; set; }


            public PatatzaakDBContext(DbContextOptions<PatatzaakDBContext> contextOptions) : base(contextOptions)
            {
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                base.OnConfiguring(optionsBuilder);
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
              
                modelBuilder.Entity<Customer>()
                    .HasMany(c => c.Orders)
                    .WithOne(o => o.Customer)
                    .HasForeignKey(o => o.CustomerId);

                modelBuilder.Entity<Category>()
                    .HasMany(c => c.products)
                    .WithOne(p => p.Category)
                    .HasForeignKey(p => p.CategoryID);

                modelBuilder.Entity<Discount>()
                    .HasOne(d => d.Product)
                    .WithOne(p => p.Discount)
                    .HasForeignKey<Discount>(d => d.ProductId);

                modelBuilder.Entity<Order>()
                    .HasMany(o => o.Orderitems)
                    .WithOne(oi => oi.Order)
                    .HasForeignKey(oi => oi.OrderId);


                //modelBuilder.Entity<Order>()
                //    .HasOne(o => o.Customer)
                //    .WithMany(c=> c.)
                //    .HasForeignKey(o => o.CustomerId);

                modelBuilder.Entity<Product>()
                    .HasOne(p => p.Category)
                    .WithMany(c => c.products)
                    .HasForeignKey(p => p.CategoryID);

                modelBuilder.Entity<Product>()
                    .HasOne(p => p.Discount)
                    .WithOne(d => d.Product)
                    .HasForeignKey<Discount>(d => d.ProductId);

                modelBuilder.Entity<Cart>()
                    .HasOne(c => c.Product)
                    .WithMany()
                    .HasForeignKey(c => c.ProductId);

                modelBuilder.Entity<Cart>()
                    .HasOne(c => c.Customer)
                    .WithMany()
                    .HasForeignKey(c => c.CustomerId);

                modelBuilder.Entity<Product>()
                    .Property(p => p.Price)
                    .HasPrecision(18, 2);

            }

    }
}
