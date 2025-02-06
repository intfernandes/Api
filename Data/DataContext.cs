
using Api.Data.Configuration;
using Api.Entities;
using Api.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
   public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<EntityAuditLog> EntityAuditLogs { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<IUser> Users { get; set; } = null!; // DbSet for IUser (TPH will use this for Customers and Members too)
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<Photo> Photos { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductCategory> ProductCategories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EntityConfigurations.ConfigureAccounts(modelBuilder);
        EntityConfigurations.ConfigureCompanies(modelBuilder);
        EntityConfigurations.ConfigureIUser(modelBuilder);
        // EntityConfigurations.ConfigureMembers(modelBuilder);
        // EntityConfigurations.ConfigureCustomers(modelBuilder);
        EntityConfigurations.ConfigureAddresses(modelBuilder);
        EntityConfigurations.ConfigureProducts(modelBuilder);
        EntityConfigurations.ConfigureCategories(modelBuilder);
        EntityConfigurations.ConfigureProductCategories(modelBuilder);
        EntityConfigurations.ConfigurePhotos(modelBuilder);
        EntityConfigurations.ConfigureOrders(modelBuilder);
        EntityConfigurations.ConfigureOrderItems(modelBuilder);
        EntityConfigurations.ConfigureEntityAuditLogs(modelBuilder);
    }

    }
}