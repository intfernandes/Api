using Api.Entities;
using Api.Entities.Users;
using Microsoft.EntityFrameworkCore; 

namespace Api.Data.Configuration
{
    public static class EntityConfigurations
    {
public static void ConfigureAccounts(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Account>(entity =>
    {
        entity.ToTable("Accounts");

        entity.Property(e => e.AccountStatus)
              .HasConversion<string>()
              .HasColumnType("nvarchar(24)");

        entity.Property(e => e.AccountType)
              .HasConversion<string>()
              .HasColumnType("nvarchar(24)");

        // Updated relationships to use generic IUser:

        entity.HasOne(e => e.User)
            .WithMany(u => u.Accounts)
            .HasForeignKey(e => e.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        // The Company - Account relationship remains unchanged:
        entity.HasOne(e => e.Company)
             .WithOne(c => c.Account)
             .HasForeignKey<Account>(e => e.CompanyId)
             .OnDelete(DeleteBehavior.Restrict);
    });
}

public static void ConfigureCompanies(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Company>(entity =>
    {
        entity.ToTable("Companies");

        // 1. One-to-One (Required) relationship with Account
        entity.HasOne(c => c.Account)
              .WithOne(a => a.Company)
              .HasForeignKey<Company>(c => c.AccountId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

        // 2. One-to-Zero-or-One (Required) relationship with Address
        entity.HasOne(c => c.Address)
              .WithOne()
              .HasForeignKey<Company>(c => c.AddressId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

        // 3. One-to-Many relationship with Member
        entity.HasMany(c => c.Members)
              .WithOne(m => m.Company)
              .HasForeignKey(m => m.CompanyId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

        // 4. One-to-Many relationship with Product
        entity.HasMany(c => c.Products)
              .WithOne(p => p.Company)
              .HasForeignKey(p => p.CompanyId)
              .OnDelete(DeleteBehavior.Cascade);

        // 5. One-to-Many relationship with Order
        entity.HasMany(c => c.Orders)
              .WithOne(o => o.Company)
              .HasForeignKey(o => o.CompanyId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);
    });
}

        public static void ConfigureIUser(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<IUser>(entity =>
    {
        entity.ToTable("Users");
        
        entity.HasKey(a => a.Id);

          entity.HasDiscriminator<string>("UserType") 
            .HasValue<Customer>("Customer")  
            .HasValue<Member>("Member");
        
        entity.Property(e => e.Gender)
              .HasConversion<string>()
              .HasColumnType("nvarchar(24)");

        // 1. One-to-Zero-or-One relationship with Address - OPTIONAL
        entity.HasOne(u => u.Address)          // IUser can have one Address
              .WithOne()
              .HasForeignKey<IUser>(u => u.AddressId) // FK is in IUser
              .IsRequired(false)
              .OnDelete(DeleteBehavior.Restrict);

        // 3. One-to-Many relationship with Photo - OPTIONAL
        entity.HasMany(u => u.Photos)           // IUser can have many Photos
              .WithOne(p => p.User)              // Photo belongs to one IUser (Navigation Property: 'User' in Photo - TO BE ADDED/RENAMED!)
              .HasForeignKey(p => p.UserId)      // FK in Photo, named 'UserId' (TO BE RENAMED!)
              .IsRequired(false)
              .OnDelete(DeleteBehavior.Cascade);

        // 4. One-to-Many relationship with Order - OPTIONAL? (Clarify Required/Optional for OrderPlacer)
        entity.HasMany(u => u.Orders)           // IUser can place many Orders
              .WithOne(o => o.Customer)      // Order is placed by one IUser (Navigation Property: 'OrderPlacer' in Order - TO BE ADDED/RENAMED!)
              .HasForeignKey(o => o.CustomerId) // FK in Order, named 'OrderPlacerId' (TO BE RENAMED!)
              .IsRequired(false) // Clarify: Should OrderPlacerId be Required or Optional?
              .OnDelete(DeleteBehavior.Restrict);
    }) ;
}

// public static void ConfigureMembers(ModelBuilder modelBuilder)
// {


//     modelBuilder.Entity<Member>(entity =>
//     {
//         entity.ToTable("Members");

//         entity.HasOne(m => m.Company)
//               .WithMany(c => c.Members)
//               .HasForeignKey(m => m.CompanyId)
//               .IsRequired()
//               .OnDelete(DeleteBehavior.Restrict);
//     });
// }

//   public static void ConfigureCustomers(ModelBuilder modelBuilder)
// {

//     modelBuilder.Entity<Customer>(entity =>
//     {
//         entity.ToTable("Customers");

//                 entity.Property(e => e.FirstName)
//                       .IsRequired()
//                       .HasMaxLength(50);

//                 entity.Property(e => e.LastName)
//                       .IsRequired()
//                       .HasMaxLength(50);

//                 entity.Property(e => e.Email)
//                       .IsRequired()
//                       .HasMaxLength(100);

//                 entity.Property(e => e.PhoneNumber)
//                       .HasMaxLength(20);

//         // 5. One-to-Many relationship with PaymentMethod
//         entity.HasMany(c => c.PaymentMethods)  // Customer has many PaymentMethods
//               .WithOne(pm => pm.Customer)    // PaymentMethod belongs to one Customer
//               .HasForeignKey(pm => pm.CustomerId) // FK in PaymentMethod, named CustomerId
//               .IsRequired(false)               // PaymentMethods are optional for Customer? (Clarify if PaymentMethod class has CustomerId nullable/required)
//               .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Customer to PaymentMethods. If Customer is deleted, delete PaymentMethods.

//         // 6. One-to-Many relationship with Review
//         entity.HasMany(c => c.Reviews)         // Customer has many Reviews
//               .WithOne(r => r.Customer)      // Review is by one Customer
//               .HasForeignKey(r => r.CustomerId)  // FK in Review, named CustomerId
//               .IsRequired(false)               // Reviews are optional for Customer? (Clarify if Review class has CustomerId nullable/required)
//               .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Customer to Reviews. If Customer is deleted, delete Reviews.

//         // 7. One-to-Many relationship with Rating
//         entity.HasMany(c => c.Ratings)         // Customer has many Ratings
//               .WithOne(rt => rt.Customer)     // Rating is by one Customer
//               .HasForeignKey(rt => rt.CustomerId) // FK in Rating, named CustomerId
//               .IsRequired(false)               // Ratings are optional for Customer? (Clarify if Rating class has CustomerId nullable/required)
//               .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Customer to Ratings. If Customer is deleted, delete Ratings.

//         // 8. One-to-Many relationship with Cart
//         entity.HasMany(c => c.Carts)           // Customer has many Carts
//               .WithOne(ct => ct.Customer)     // Cart belongs to one Customer
//               .HasForeignKey(ct => ct.CustomerId) // FK in Cart, named CustomerId
//               .IsRequired(false)               // Carts are optional for Customer? (Clarify if Cart class has CustomerId nullable/required)
//               .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Customer to Carts. If Customer is deleted, delete Carts.

//         // 9. One-to-Many relationship with Wishlist
//         entity.HasMany(c => c.Wishlists)       // Customer has many Wishlists
//               .WithOne(wl => wl.Customer)     // Wishlist belongs to one Customer
//               .HasForeignKey(wl => wl.CustomerId) // FK in Wishlist, named CustomerId
//               .IsRequired(false)               // Wishlists are optional for Customer? (Clarify if Wishlist class has CustomerId nullable/required)
//               .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Customer to Wishlists. If Customer is deleted, delete Wishlists.

//         // 10. One-to-Many relationship with Subscription
//         entity.HasMany(c => c.Subscriptions)   // Customer has many Subscriptions
//               .WithOne(s => s.Customer)       // Subscription belongs to one Customer
//               .HasForeignKey(s => s.CustomerId)  // FK in Subscription, named CustomerId
//               .IsRequired(false)               // Subscriptions are optional for Customer? (Clarify if Subscription class has CustomerId nullable/required)
//               .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Customer to Subscriptions. If Customer is deleted, delete Subscriptions.

//         // 11. One-to-Many relationship with Feedback
//         entity.HasMany(c => c.Feedbacks)       // Customer has many Feedbacks
//               .WithOne(fb => fb.Customer)     // Feedback is from one Customer
//               .HasForeignKey(fb => fb.CustomerId) // FK in Feedback, named CustomerId
//               .IsRequired(false)               // Feedbacks are optional for Customer? (Clarify if Feedback class has CustomerId nullable/required)
//               .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Customer to Feedbacks. If Customer is deleted, delete Feedbacks.

//         // 12. One-to-Many relationship with Complaint
//         entity.HasMany(c => c.Complaints)      // Customer has many Complaints
//               .WithOne(comp => comp.Customer) // Complaint is by one Customer
//               .HasForeignKey(comp => comp.CustomerId) // FK in Complaint, named CustomerId
//               .IsRequired(false)               // Complaints are optional for Customer? (Clarify if Complaint class has CustomerId nullable/required)
//               .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Customer to Complaints. If Customer is deleted, delete Complaints.

//         // 13. One-to-Many relationship with Refund
//         entity.HasMany(c => c.Refunds)         // Customer has many Refunds
//               .WithOne(refd => refd.Customer) // Refund is for one Customer
//               .HasForeignKey(refd => refd.CustomerId) // FK in Refund, named CustomerId
//               .IsRequired(false)               // Refunds are optional for Customer? (Clarify if Refund class has CustomerId nullable/required)
//               .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Customer to Refunds. If Customer is deleted, delete Refunds.

//         // 14. One-to-Many relationship with Return
//         entity.HasMany(c => c.Returns)         // Customer has many Returns
//               .WithOne(ret => ret.Customer)   // Return is by one Customer
//               .HasForeignKey(ret => ret.CustomerId) // FK in Return, named CustomerId
//               .IsRequired(false)               // Returns are optional for Customer? (Clarify if Return class has CustomerId nullable/required)
//               .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Customer to Returns. If Customer is deleted, delete Returns.

//         // 15. One-to-Many relationship with Report
//         entity.HasMany(c => c.Reports)         // Customer has many Reports
//               .WithOne(rep => rep.Customer)   // Report is by one Customer
//               .HasForeignKey(rep => rep.CustomerId) // FK in Report, named CustomerId
//               .IsRequired(false)               // Reports are optional for Customer? (Clarify if Report class has CustomerId nullable/required)
//               .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Customer to Reports. If Customer is deleted, delete Reports.

//         // 16. One-to-Many relationship with Chat
//         entity.HasMany(c => c.Chats)           // Customer has many Chats
//               .WithOne(cht => cht.Customer)   // Chat is started by one Customer
//               .HasForeignKey(cht => cht.CustomerId) // FK in Chat, named CustomerId
//               .IsRequired(false)               // Chats are optional for Customer? (Clarify if Chat class has CustomerId nullable/required)
//               .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Customer to Chats. If Customer is deleted, delete Chats.

//         // 17. One-to-Many relationship with Conversation
//         entity.HasMany(c => c.Conversations)   // Customer has many Conversations
//               .WithOne(conv => conv.Customer) // Conversation is started by/belongs to one Customer
//               .HasForeignKey(conv => conv.CustomerId) // FK in Conversation, named CustomerId
//               .IsRequired(false)               // Conversations are optional for Customer? (Clarify if Conversation class has CustomerId nullable/required)
//               .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Customer to Conversations. If Customer is deleted, delete Conversations.

//         // 18. One-to-Many relationship with Message
//         entity.HasMany(c => c.Messages)        // Customer has many Messages
//               .WithOne(msg => msg.Customer)   // Message is sent by one Customer
//               .HasForeignKey(msg => msg.CustomerId) // FK in Message, named CustomerId
//               .IsRequired(false)               // Messages are optional for Customer? (Clarify if Message class has CustomerId nullable/required)
//               .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Customer to Messages. If Customer is deleted, delete Messages.

//         // 19. One-to-Many relationship with Notification
//         entity.HasMany(c => c.Notifications)   // Customer has many Notifications
//               .WithOne(notif => notif.Customer) // Notification is for one Customer
//               .HasForeignKey(notif => notif.CustomerId) // FK in Notification, named CustomerId
//               .IsRequired(false)               // Notifications are optional for Customer? (Clarify if Notification class has CustomerId nullable/required)
//               .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Customer to Notifications. If Customer is deleted, delete Notifications.
//     });
// }

public static void ConfigureAddresses(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Address>(entity =>
    {
        entity.ToTable("Addresses"); // Explicit table name

        // Properties configuration (DataAnnotations already handle most of these, but we can re-assert here if needed for clarity or specific DB types)

        entity.Property(e => e.Street)
              .IsRequired()             // As defined with [Required] in Address class
              .HasMaxLength(255);       // As defined with [MaxLength(255)]

        entity.Property(e => e.City)
              .IsRequired()             // As defined with [Required]
              .HasMaxLength(100);       // As defined with [MaxLength(100)]

        entity.Property(e => e.State)
              .IsRequired()             // As defined with [Required]
              .HasMaxLength(50);        // As defined with [MaxLength(50)]

        entity.Property(e => e.ZipCode)
              .IsRequired(false)         // ZipCode is optional - no [Required] in Address class
              .HasMaxLength(10);        // As defined with [MaxLength(10)] - although might consider increasing for international zip codes if needed

        // No relationships to configure directly in Address itself, as we decided on one-way navigation TO Address in other entities (Company, IUser, Order).
        // The relationships involving Address are configured in ConfigureCompanies, ConfigureIUser, and ConfigureOrders (for ShippingAddress and BillingAddress).

    });
}

public static void ConfigureProducts(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Product>(entity =>
    {
        entity.ToTable("Products"); // Explicit table name

        // Properties configuration

        entity.Property(e => e.Name)
              .IsRequired()             // As defined with [Required] in Product class
              .HasMaxLength(255);       // As defined with [MaxLength(255)]

        entity.Property(e => e.Description)
              .IsRequired(false)        // Description is optional - no [Required] in Product class
              .HasMaxLength(500);       // As defined with [MaxLength(500)]

        entity.Property(e => e.Price)
              .IsRequired()             // Price is implicitly required as it's a decimal (non-nullable value type)
              .HasColumnType("decimal(18, 2)"); // Recommended to explicitly set decimal precision and scale
                                                 // 18,2 is a common default for currency values. Adjust as needed.
              // .HasPrecision(18, 2); // Alternative for setting precision and scale in EF Core 7+

        // Relationships configuration

        // 1. Many-to-One relationship with Company (Product belongs to one Company)
        entity.HasOne(p => p.Company)         // Product belongs to one Company (navigation property in Product)
              .WithMany(c => c.Products)        // Company has many Products (collection navigation in Company)
              .HasForeignKey(p => p.CompanyId)  // FK is in Product, named CompanyId (nullable in Product class - relationship is optional)
              .IsRequired(false)                 // Relationship is OPTIONAL - Product does not necessarily need to belong to a Company immediately upon creation.
                                                  // Consider making it Required if business logic dictates Products always belong to a Company.
              .OnDelete(DeleteBehavior.Restrict); // Restrict delete from Company to Products. If Company is deleted, prevent deletion if there are associated Products.
                                                   // Consider Cascade if deleting a Company should also imply removing its Products.

        // 2. Many-to-Many relationship with Category (Product has many Categories, Category has many Products)
        entity.HasMany(p => p.Categories)      // Product has many Categories (collection navigation in Product)
              .WithMany(c => c.Products)        // Category has many Products (collection navigation in Category)
              .UsingEntity<ProductCategory>(     // Using the join entity 'ProductCategory'
                  j => j
                      .HasOne(pc => pc.Category)  // ProductCategory has one Category
                      .WithMany()                 // Category doesn't have a direct collection navigation to ProductCategory (optional)
                      .HasForeignKey(pc => pc.CategoryId) // FK for Category in ProductCategory
                      .OnDelete(DeleteBehavior.Cascade), // Cascade delete from Category to ProductCategory - If a Category is deleted, remove ProductCategory entries related to it.
                  j => j
                      .HasOne(pc => pc.Product)   // ProductCategory has one Product
                      .WithMany()                 // Product doesn't have a direct collection navigation to ProductCategory (optional)
                      .HasForeignKey(pc => pc.ProductId)  // FK for Product in ProductCategory
                      .OnDelete(DeleteBehavior.Cascade), // Cascade delete from Product to ProductCategory - If a Product is deleted, remove ProductCategory entries related to it.
                  j =>
                  {
                      j.ToTable("ProductCategories"); // Explicit name for the join table (optional, but good practice)
                      j.HasKey(pc => new { pc.ProductId, pc.CategoryId }); // Composite primary key for ProductCategory (ProductId + CategoryId)
                  });
    });
}

public static void ConfigureCategories(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Category>(entity =>
    {
        entity.ToTable("Categories"); // Explicit table name

        // Properties configuration

        entity.Property(e => e.Name)
              .IsRequired()             // As defined with [Required] in Category class
              .HasMaxLength(100);       // As defined with [MaxLength(100)]

        entity.Property(e => e.Description)
              .IsRequired(false)        // Description is optional - no [Required] in Category class
              .HasMaxLength(500);       // As defined with [MaxLength(500)]

        // Relationships configuration

        // 1. Many-to-Many relationship with Product (Category has many Products, Product has many Categories) - Configured from Product side in ConfigureProducts.
        //    Here, we only need to configure the Category side of the many-to-many using UsingEntity<ProductCategory>.

        entity.HasMany(c => c.Products)      // Category has many Products (collection navigation in Category)
              .WithMany(p => p.Categories)    // Product has many Categories (collection navigation in Product)
              .UsingEntity<ProductCategory>(    // Using the join entity 'ProductCategory'
                  j => j
                      .HasOne(pc => pc.Product)  // ProductCategory has one Product
                      .WithMany()                // Product doesn't have a direct collection navigation to ProductCategory (optional)
                      .HasForeignKey(pc => pc.ProductId) // FK for Product in ProductCategory
                      .OnDelete(DeleteBehavior.Cascade), // Cascade delete from Product to ProductCategory - If a Product is deleted, remove ProductCategory entries related to it.
                  j => j
                      .HasOne(pc => pc.Category) // ProductCategory has one Category
                      .WithMany()                // Category doesn't have a direct collection navigation to ProductCategory (optional)
                      .HasForeignKey(pc => pc.CategoryId) // FK for Category in ProductCategory
                      .OnDelete(DeleteBehavior.Cascade), // Cascade delete from Category to ProductCategory - If a Category is deleted, remove ProductCategory entries related to it.
                  j =>
                  {
                      j.ToTable("ProductCategories"); // Explicit name for the join table (optional, but good practice)
                      j.HasKey(pc => new { pc.ProductId, pc.CategoryId }); // Composite primary key for ProductCategory (ProductId + CategoryId)
                  });
    });
}

public static void ConfigureProductCategories(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<ProductCategory>(entity =>
    {
        entity.ToTable("ProductCategories"); // Explicit table name - though already defined in UsingEntity in Product and Category, good to have here too for clarity

        // Composite Primary Key - already defined in UsingEntity in Product and Category, good to have here too for clarity
        entity.HasKey(pc => new { pc.ProductId, pc.CategoryId });

        // Foreign Key Relationships - Explicitly configuring them here for clarity, even though they are also configured in UsingEntity in Product and Category
        // 1. Relationship with Product
        entity.HasOne(pc => pc.Product)      // ProductCategory has one Product
              .WithMany()                    // Product doesn't have a direct collection navigation to ProductCategory
              .HasForeignKey(pc => pc.ProductId) // FK for Product is ProductId
              .OnDelete(DeleteBehavior.Cascade); // Cascade delete - If a Product is deleted, delete related ProductCategory entries

        // 2. Relationship with Category
        entity.HasOne(pc => pc.Category)     // ProductCategory has one Category
              .WithMany()                    // Category doesn't have a direct collection navigation to ProductCategory
              .HasForeignKey(pc => pc.CategoryId) // FK for Category is CategoryId
              .OnDelete(DeleteBehavior.Cascade); // Cascade delete - If a Category is deleted, delete related ProductCategory entries

        // No properties to configure on ProductCategory itself other than the composite key and FKs.
        // ProductCategory entity is primarily used to represent the join table and its relationships.
    });
}

public static void ConfigurePhotos(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Photo>(entity =>
    {
        entity.ToTable("Photos");

   entity.HasKey(p => p.Id); // Assuming Id is the PK


         entity.Property(p => p.ImageUrl)
                   .IsRequired()
                   .HasMaxLength(2048);

        // Updated relationships to use generic IUser:
        entity.HasOne(p => p.User)           // Photo belongs to one IUser (now using 'User' navigation)
              .WithMany(u => u.Photos)         // IUser can have many Photos
              .HasForeignKey(p => p.UserId)     // FK is UserId (renamed from CustomerId/MemberId)
              .IsRequired(false)                 // Relationship is OPTIONAL - IUser can have zero or more Photos
              .OnDelete(DeleteBehavior.Cascade);

        // The Product - Photo relationship remains unchanged:
        entity.HasOne(p => p.Product)
              .WithMany(prod => prod.Photos)
              .HasForeignKey(p => p.ProductId)
              .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(p => p.Company)
                   .WithMany(comp => comp.Photos) // Assuming Company has a Photos collection navigation property
                   .HasForeignKey(p => p.CompanyId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Cascade); // Or choose appropriate DeleteBehavior
    });
}

public static void ConfigureOrders(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Order>(entity =>
    {
        entity.ToTable("Orders");

        entity.Property(e => e.Status)
              .HasConversion<string>()
              .HasColumnType("nvarchar(24)");

        // Updated relationships to use generic IUser (OrderPlacer):
        entity.HasOne(o => o.Customer)     // Order is placed by one IUser (now using 'OrderPlacer' navigation)
              .WithMany(u => u.Orders)         // IUser can place many Orders
              .HasForeignKey(o => o.CustomerId) // FK is OrderPlacerId (renamed from CustomerId)
              .IsRequired(false) // Clarify: Should OrderPlacerId be Required or Optional? - Set to false for now.
              .OnDelete(DeleteBehavior.Restrict);

        // The Member, Company, ShippingAddress, BillingAddress relationships remain unchanged:
        entity.HasOne(o => o.Member)
              .WithMany() // Member doesn't have a navigation collection for Orders
              .HasForeignKey(o => o.MemberId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(o => o.Company)
              .WithMany(c => c.Orders)
              .HasForeignKey(o => o.CompanyId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(o => o.ShippingAddress)
              .WithMany()
              .HasForeignKey(o => o.ShippingAddressId)
              .IsRequired(false)
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(o => o.BillingAddress)
              .WithMany()
              .HasForeignKey(o => o.BillingAddressId)
              .IsRequired(false)
              .OnDelete(DeleteBehavior.Restrict);
    });
}

public static void ConfigureOrderItems(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<OrderItem>(entity =>
    {
        entity.ToTable("OrderItems"); // Explicit table name

        // Properties configuration

        entity.Property(e => e.Quantity)
              .IsRequired();             // Quantity is implicitly required as it's an int (non-nullable value type)
              // .IsRequired() is redundant for non-nullable value types but adds clarity

        entity.Property(e => e.Price)
              .IsRequired()             // Price is implicitly required as it's a decimal (non-nullable value type)
              .HasColumnType("decimal(18, 2)"); // Explicitly set decimal precision and scale (as we did for Product Price)


        // Relationships configuration

        // 1. Many-to-One relationship with Order (OrderItem belongs to one Order)
        entity.HasOne(oi => oi.Order)           // OrderItem belongs to one Order (navigation property in OrderItem)
              .WithMany(o => o.OrderItems)      // Order has many OrderItems (collection navigation in Order)
              .HasForeignKey(oi => oi.OrderId)   // FK is in OrderItem, named OrderId (Required in OrderItem class)
              .IsRequired()                      // Relationship is REQUIRED - OrderItem MUST belong to an Order
              .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Order to OrderItems. If Order is deleted, delete its OrderItems.
                                                   // Typically, OrderItems are considered part of the Order and should be deleted if the Order is deleted.

        // 2. Many-to-One relationship with Product (OrderItem is for one Product)
        entity.HasOne(oi => oi.Product)         // OrderItem is for one Product (navigation property in OrderItem)
              .WithMany()                        // Product does NOT have a collection navigation for OrderItems (one-way navigation from OrderItem to Product is sufficient)
              .HasForeignKey(oi => oi.ProductId) // FK is in OrderItem, named ProductId (Required in OrderItem class)
              .IsRequired()                      // Relationship is REQUIRED - OrderItem MUST be for a Product
              .OnDelete(DeleteBehavior.Restrict); // Restrict delete from Product to OrderItems. If Product is deleted, prevent deletion if there are associated OrderItems.
                                                   // We generally don't want to delete Products if they are part of historical orders.
                                                   // Consider if SetNull is more appropriate if you want to keep OrderItems but disassociate them from a deleted Product.
                                                   // However, Restrict is generally safer to prevent accidental data corruption in order history.
    });
}

public static void ConfigureEntityAuditLogs(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<EntityAuditLog>(entity =>
    {
        entity.ToTable("EntityAuditLogs"); // Explicit table name

        // Properties configuration (no changes needed)

        entity.Property(e => e.EntityType)
              .IsRequired()
              .HasMaxLength(255);

        entity.Property(e => e.EntityId)
              .IsRequired(false);

        entity.Property(e => e.ActionType)
              .IsRequired()
              .HasMaxLength(50);

        entity.Property(e => e.Changes)
              .IsRequired(false);

        entity.Property(e => e.Timestamp)
              .IsRequired();

        entity.Property(e => e.UserId)
              .IsRequired(false); // UserId remains optional in EntityAuditLog


        // Relationships configuration - Now with explicit relationship to IUser

        entity.HasOne(e => e.User) // Now configuring the relationship to IUser - Navigation property 'User' in EntityAuditLog
              .WithMany()       // IUser doesn't necessarily need a navigation collection to EntityAuditLogs
              .HasForeignKey(e => e.UserId) // FK is UserId in EntityAuditLog
              .IsRequired(false) // UserId is optional in EntityAuditLog (as configured above) - Relationship remains optional
              .OnDelete(DeleteBehavior.Restrict); // Restrict delete from IUser to EntityAuditLogs
              // Restrict is generally appropriate for audit logs - deleting a User should not delete their audit history.

    });
}

        // No configuration method needed for BaseEntity and AuditableBaseEntity as they are abstract base classes and won't have their own tables.
    }
}




// using Api.Entities;
// using Api.Entities.Users;
// using Microsoft.EntityFrameworkCore;

// namespace Api.Data
// {
//         public class ModelBuilders
//         {
         

//         public static void ConfigureAccounts(ModelBuilder modelBuilder)
//         {
//             modelBuilder.Entity<Account>(b =>
//             {
//                 b.Property(o => o.AccountStatus)
//                  .HasConversion<string>();

//                 b.Property(o => o.AccountType)
//                  .HasConversion<string>();

//                 b.HasOne(p => p.Member)
//                     .WithMany(m => m.Accounts)
//                     .HasForeignKey(p => p.MemberId)
//                     .OnDelete(DeleteBehavior.Restrict);

//                 b.HasOne(p => p.Customer)
//                     .WithMany(m => m.Accounts)
//                     .HasForeignKey(p => p.CustomerId)
//                     .OnDelete(DeleteBehavior.Restrict);
//             });
//         }
       
//               public static void ConfigureCompanies(ModelBuilder modelBuilder)
//               {
//                  modelBuilder.Entity<Company>(b =>
//                  {

//             modelBuilder.Entity<Company>().ToTable("Companies");
      
//             b.HasOne(c => c.Account)
//              .WithOne(a => a.Company)
//              .HasForeignKey<Account>(a => a.CompanyId);  

//             b.HasOne(u => u.Address)
//              .WithOne(a => a.Company)
//              .HasForeignKey<Address>(a => a.CompanyId)
//              .OnDelete(DeleteBehavior.SetNull);

//              b.HasMany(c => c.Members)
//             .WithOne(m => m.Company)
//             .HasForeignKey(m => m.CompanyId);

//             b.HasMany(c => c.Products)
//             .WithOne(p => p.Company)
//             .HasForeignKey(p => p.CompanyId);

//              b.HasMany(c => c.Orders)
//             .WithOne(o => o.Company)
//             .HasForeignKey(o => o.CompanyId);
//         }); 
         
//     }


//     public static void ConfigureMembers(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<Member>(b =>
//         {
//             modelBuilder.Entity<Member>().ToTable("Members");

           
//             b.Property(o => o.Gender)
//              .HasConversion<string>();  

//             b.HasMany(c => c.Accounts)
//              .WithOne(a => a.Member)
//              .HasForeignKey(a => a.MemberId)  
//              .OnDelete(DeleteBehavior.Restrict);  
     
//             b.HasOne(c => c.Address)
//              .WithOne(a => a.Member)
//              .HasForeignKey<Address>(a => a.MemberId)  
//              .OnDelete(DeleteBehavior.Restrict);  

//             b.HasMany(m => m.Photos)
//              .WithOne()
//              .HasForeignKey(p => p.MemberId)
//              .OnDelete(DeleteBehavior.SetNull);

//             b.HasOne(u => u.HighlightPhoto)
//                 .WithOne()
//                 .HasForeignKey<Member>(u => u.HighlightPhotoId)
//                 .OnDelete(DeleteBehavior.SetNull);


//         });
//     }

//    public static void ConfigureCustomer(ModelBuilder modelBuilder)
// {
//         modelBuilder.Entity<Customer>(b =>
//         {
//             modelBuilder.Entity<Customer>().ToTable("Customers"); 

//             b.Property(o => o.Gender)
//              .HasConversion<string>(); 


//             b.HasMany(u => u.Accounts)
//              .WithOne(a => a.Customer ) 
//              .HasForeignKey(a => a.CustomerId)
//              .OnDelete(DeleteBehavior.Restrict); 

//             b.HasOne(u => u.Address)
//              .WithOne(a => a.Customer)
//              .HasForeignKey<Address>(a => a.CustomerId)
//              .OnDelete(DeleteBehavior.SetNull);

//             b.HasMany(u => u.Photos)
//              .WithOne()
//              .HasForeignKey(p => p.CustomerId)
//              .OnDelete(DeleteBehavior.SetNull);

//             b.HasOne(u => u.HighlightPhoto)
//                 .WithOne()
//                 .HasForeignKey<Customer>(u => u.HighlightPhotoId)
//                 .OnDelete(DeleteBehavior.SetNull);

//               b.HasMany(c => c.PaymentMethods)
//              .WithOne(p => p.Customer)
//              .HasForeignKey(p => p.CustomerId);

//             b.HasMany(c => c.Ratings)
//              .WithOne(p => p.Customer)
//              .HasForeignKey(p => p.CustomerId);

//                 b.HasMany(c => c.Carts)
//              .WithOne(p => p.Customer)
//              .HasForeignKey(p => p.CustomerId);

//                 b.HasMany(c => c.Wishlists)
//              .WithOne(p => p.Customer)
//              .HasForeignKey(p => p.CustomerId);

//                 b.HasMany(c => c.Subscriptions)
//              .WithOne(p => p.Customer)
//              .HasForeignKey(p => p.CustomerId);

//                 b.HasMany(c => c.Feedbacks)
//              .WithOne(p => p.Customer)
//              .HasForeignKey(p => p.CustomerId);

//                 b.HasMany(c => c.Complaints)
//              .WithOne(p => p.Customer)
//              .HasForeignKey(p => p.CustomerId);

//                 b.HasMany(c => c.Refunds)
//              .WithOne(p => p.Customer)
//              .HasForeignKey(p => p.CustomerId);

//                 b.HasMany(c => c.Returns)
//              .WithOne(p => p.Customer)
//              .HasForeignKey(p => p.CustomerId);

//                 b.HasMany(c => c.Reports)
//              .WithOne(p => p.Customer)
//              .HasForeignKey(p => p.CustomerId);

//                 b.HasMany(c => c.Chats)
//              .WithOne(p => p.Customer)
//              .HasForeignKey(p => p.CustomerId);

//                 b.HasMany(c => c.Conversations)
//              .WithOne(p => p.Customer)
//              .HasForeignKey(p => p.CustomerId);

//                 b.HasMany(c => c.Messages)
//              .WithOne(p => p.Customer)
//              .HasForeignKey(p => p.CustomerId);

//                 b.HasMany(c => c.Notifications)
//              .WithOne(p => p.Customer)
//              .HasForeignKey(p => p.CustomerId);

       

//         });
// }

//     public static void ConfigureProducts(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<Product>(b =>
//             { 
//                 b.HasOne(p => p.Company)
//                 .WithMany(c => c.Products)
//                 .HasForeignKey(p => p.CompanyId);

//                 b.HasMany(p => p.Photos)
//                     .WithOne(p => p.Product)
//                     .HasForeignKey(p => p.ProductId)
//                     .OnDelete(DeleteBehavior.Cascade); 

//                     b.HasMany(p => p.Categories)
//                     .WithMany(c => c.Products)
//                     .UsingEntity<ProductCategory>(
//                 j => j.HasKey(pc => new { pc.ProductId , pc.CategoryId }));

                    
            
//         }); 

//     }





//     public static void ConfigurePhoto(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<Photo>(b =>
//         {
//                 b.HasOne(p => p.Member)
//                     .WithMany(m => m.Photos)
//                     .HasForeignKey(p => p.MemberId)
//                     .OnDelete(DeleteBehavior.Restrict);

//                 b.HasOne(p => p.Customer)
//                     .WithMany(m => m.Photos)
//                     .HasForeignKey(p => p.CustomerId)
//                     .OnDelete(DeleteBehavior.Restrict);

//             b.HasOne(p => p.Product)
//                 .WithMany(p => p.Photos)
//                 .HasForeignKey(p => p.ProductId)
//                 .OnDelete(DeleteBehavior.Restrict);
//         });
//     }

//         public static void ConfigureAddresses(ModelBuilder modelBuilder)
//         {
//             modelBuilder.Entity<Address>(b =>
//             {
//                 b.HasKey(a => a.Id);
                
//                 b.HasOne(a => a.Company)
//                     .WithOne(u => u.Address)
//                     .HasForeignKey<Company>(a => a.AddressId)
//                     .OnDelete(DeleteBehavior.SetNull);

//                 b.HasOne(a => a.Member)
//                     .WithOne(u => u.Address)
//                     .HasForeignKey<Member>(a => a.AddressId)
//                     .OnDelete(DeleteBehavior.SetNull);

//                 b.HasOne(a => a.Customer)
//                     .WithOne(u => u.Address)
//                     .HasForeignKey<Customer>(a => a.AddressId)
//                     .OnDelete(DeleteBehavior.SetNull);
//             });
//         }

//       public static void ConfigureOrders(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<Order>(b =>
//     {

//         b.HasKey(o => o.Id);

//           b.Property(o => o.Status)
//             .HasConversion<string>();  

//         b.HasOne(o => o.Customer)
//             .WithMany(c => c.Orders)
//             .HasForeignKey(o => o.CustomerId)
//             .OnDelete(DeleteBehavior.Restrict);

//         b.HasOne(o => o.Member)
//             .WithMany(m => m.Orders)
//             .HasForeignKey(o => o.MemberId)
//             .OnDelete(DeleteBehavior.Restrict);

//         b.HasMany(o => o.OrderItems)
//             .WithOne(oi => oi.Order)
//             .HasForeignKey(oi => oi.OrderId)
//             .OnDelete(DeleteBehavior.Cascade); 

//         b.Property(o => o.Status)
//         .HasConversion<string>(); 
//     });

//        modelBuilder.Entity<OrderItem>(b =>
//     {
//         b.HasOne(oi => oi.Order)
//             .WithMany(o => o.OrderItems)
//             .HasForeignKey(oi => oi.OrderId)
//             .OnDelete(DeleteBehavior.Cascade); 

//         b.HasOne(oi => oi.Product)
//             .WithMany()
//             .HasForeignKey(oi => oi.ProductId)
//             .OnDelete(DeleteBehavior.Restrict); 
//     });
//     }}
// }