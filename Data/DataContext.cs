
using Api.Entities;
using Api.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
    public DbSet<Photo> Photos { get; set; } = null!;


           protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ModelBuilders.ConfigureCompanies(modelBuilder);
        ModelBuilders.ConfigureMembers(modelBuilder);
        ModelBuilders.ConfigureCustomer(modelBuilder);
        ModelBuilders.ConfigureProducts(modelBuilder);
        ModelBuilders.ConfigurePhoto(modelBuilder); 
        ModelBuilders.ConfigureOrder(modelBuilder); 
    }

    }
}