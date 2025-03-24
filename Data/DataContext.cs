
using Api.Data.Configuration;
using Api.Entities;

using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
   public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Store> Stores { get; set; } = null!;
    public DbSet<EntityAuditLog> EntityAuditLogs { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<Photo> Photos { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductCategory> ProductCategories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EntityConfigurations.ConfigureAccounts(modelBuilder);
        EntityConfigurations.ConfigureCompanies(modelBuilder);
        EntityConfigurations.ConfigureUsers(modelBuilder); 
        EntityConfigurations.ConfigureAddresses(modelBuilder);
        EntityConfigurations.ConfigureProducts(modelBuilder);
        EntityConfigurations.ConfigureCategories(modelBuilder);
        EntityConfigurations.ConfigureProductCategories(modelBuilder); 
        EntityConfigurations.ConfigureOrders(modelBuilder);
        EntityConfigurations.ConfigureOrderItems(modelBuilder);
        EntityConfigurations.ConfigureEntityAuditLogs(modelBuilder);
    }

    }
}