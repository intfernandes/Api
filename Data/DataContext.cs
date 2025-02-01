
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
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<Rating> Ratings { get; set; } = null!;
    public DbSet<Cart> Carts { get; set; } = null!;
    public DbSet<Wishlist> Wishlists { get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<Feedback> Feedbacks { get; set; } = null!;
    public DbSet<Complaint> Complaints { get; set; } = null!;
    public DbSet<Refund> Refunds { get; set; } = null!;
    public DbSet<Return> Returns { get; set; } = null!;
    public DbSet<Report> Reports { get; set; } = null!;
    public DbSet<Chat> Chats { get; set; } = null!;
    public DbSet<Conversation> Conversations { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;

    


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