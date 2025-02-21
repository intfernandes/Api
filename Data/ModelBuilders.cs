using Api.Entities;

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

        // The Domain - Account relationship remains unchanged:
        entity.HasOne(e => e.Domain)
             .WithOne(c => c.Account)
             .HasForeignKey<Account>(e => e.DomainId)
             .OnDelete(DeleteBehavior.Restrict);
    });
}

public static void ConfigureCompanies(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Domain>(entity =>
    {
            entity.ToTable("Domains");
 
            entity.HasMany(c => c.Members)
              .WithOne(m => m.Domain)
              .HasForeignKey(m => m.DomainId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);
  
            entity.HasMany(c => c.Products)
              .WithOne(p => p.Domain)
              .HasForeignKey(p => p.DomainId)
              .OnDelete(DeleteBehavior.Cascade);
 
            entity.HasMany(c => c.Orders)
              .WithOne(o => o.Domain)
              .HasForeignKey(o => o.DomainId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(c => c.Photos)    
              .WithOne(p => p.Domain)     
              .HasForeignKey(p => p.DomainId) 
              .OnDelete(DeleteBehavior.Cascade);
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

            entity.HasMany(c => c.Photos)    
              .WithOne(p => p.User)     
              .HasForeignKey(p => p.UserId) 
              .OnDelete(DeleteBehavior.Cascade);
    }) ;
}

public static void ConfigureAddresses(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Address>(entity =>
    {
      entity.ToTable("Addresses"); 

      entity.Property(e => e.Street)
              .IsRequired()            
              .HasMaxLength(255);      

      entity.Property(e => e.City)
              .IsRequired()           
              .HasMaxLength(100);     

      entity.Property(e => e.State)
              .IsRequired()             
              .HasMaxLength(50);      

      entity.Property(e => e.ZipCode)
              .IsRequired(false)        
              .HasMaxLength(10);     

      entity.HasOne(a => a.Domain)      
              .WithOne(c => c.Address)      
              .HasForeignKey<Domain>(c => c.AddressId) 
              .IsRequired(false)                
              .OnDelete(DeleteBehavior.Restrict);  

      entity.HasOne(a => a.User)        
              .WithOne(u => u.Address)       
              .HasForeignKey<IUser>(u => u.AddressId) 
              .IsRequired(false)                
              .OnDelete(DeleteBehavior.Restrict);
    });
}

public static void ConfigureProducts(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Product>(entity =>
    {
            entity.ToTable("Products"); 

            entity.Property(e => e.Name)
              .IsRequired()           
              .HasMaxLength(255);    

            entity.Property(e => e.Description)
              .IsRequired(false)     
              .HasMaxLength(500);     

            entity.Property(e => e.Price)
              .IsRequired()           
              .HasColumnType("decimal(18, 2)");
                                                 
            entity.HasOne(p => p.Domain)      
              .WithMany(c => c.Products)        
              .HasForeignKey(p => p.DomainId)  
              .IsRequired(false)                
              .OnDelete(DeleteBehavior.Restrict); 

            entity.HasMany(c => c.Photos)    
              .WithOne(p => p.Product)     
              .HasForeignKey(p => p.ProductId) 
              .OnDelete(DeleteBehavior.Cascade);

     
            entity.HasMany(p => p.Categories)    
              .WithMany(c => c.Products)       
              .UsingEntity<ProductCategory>(     
                  j => j
                      .HasOne(pc => pc.Category)  
                      .WithMany()               
                      .HasForeignKey(pc => pc.CategoryId) 
                      .OnDelete(DeleteBehavior.Cascade), 
                  j => j
                      .HasOne(pc => pc.Product)  
                      .WithMany()               
                      .HasForeignKey(pc => pc.ProductId)  
                      .OnDelete(DeleteBehavior.Cascade), 
                  j =>
                  {
                      j.ToTable("ProductCategories"); 
                      j.HasKey(pc => new { pc.ProductId, pc.CategoryId }); 
                  });
    });
}

public static void ConfigureCategories(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Category>(entity =>
    {
            entity.ToTable("Categories");  

            entity.Property(e => e.Name)
              .IsRequired()            
              .HasMaxLength(100);      

            entity.Property(e => e.Description)
              .IsRequired(false)       
              .HasMaxLength(500);    

            entity.HasMany(c => c.Photos)    
              .WithOne(p => p.Category)     
              .HasForeignKey(p => p.CategoryId) 
              .OnDelete(DeleteBehavior.Cascade);
 
            entity.HasMany(c => c.Products)       
              .WithMany(p => p.Categories)    
              .UsingEntity<ProductCategory>(    
                  j => j
                      .HasOne(pc => pc.Product)   
                      .WithMany()                
                      .HasForeignKey(pc => pc.ProductId)  
                      .OnDelete(DeleteBehavior.Cascade),  
                  j => j
                      .HasOne(pc => pc.Category)  
                      .WithMany()              
                      .HasForeignKey(pc => pc.CategoryId)  
                      .OnDelete(DeleteBehavior.Cascade),  
                  j =>
                  {
                      j.ToTable("ProductCategories");  
                      j.HasKey(pc => new { pc.ProductId, pc.CategoryId });  
                  });
    });
}

public static void ConfigureProductCategories(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<ProductCategory>(entity =>
    {
        entity.ToTable("ProductCategories"); 

        entity.HasKey(pc => new { pc.ProductId, pc.CategoryId });

        entity.HasOne(pc => pc.Product)      
              .WithMany()                  
              .HasForeignKey(pc => pc.ProductId) 
              .OnDelete(DeleteBehavior.Cascade); 
              
        entity.HasOne(pc => pc.Category)    
              .WithMany()                   
              .HasForeignKey(pc => pc.CategoryId) 
              .OnDelete(DeleteBehavior.Cascade); 
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

        entity.HasOne(o => o.Customer)     
              .WithMany(u => u.Orders)      
              .HasForeignKey(o => o.CustomerId) 
              .IsRequired(true) 
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(o => o.Member)
              .WithMany() 
              .HasForeignKey(o => o.MemberId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(o => o.Domain)
              .WithMany(c => c.Orders)
              .HasForeignKey(o => o.DomainId)
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
        entity.ToTable("OrderItems");

        entity.Property(e => e.Quantity)
              .IsRequired();      
        entity.Property(e => e.Price)
              .IsRequired()             
              .HasColumnType("decimal(18, 2)"); 

        entity.HasOne(oi => oi.Order)        
              .WithMany(o => o.OrderItems)   
              .HasForeignKey(oi => oi.OrderId)  
              .IsRequired()                   
              .OnDelete(DeleteBehavior.Cascade); 
        entity.HasOne(oi => oi.Product)         
              .WithMany()       
              .HasForeignKey(oi => oi.ProductId) 
              .IsRequired()                   
              .OnDelete(DeleteBehavior.Restrict); 
    });
}

public static void ConfigureEntityAuditLogs(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<EntityAuditLog>(entity =>
    {
        entity.ToTable("EntityAuditLogs"); 

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
              .IsRequired(false); 

        entity.HasOne(e => e.User)
              .WithMany()       
              .HasForeignKey(e => e.UserId) 
              .IsRequired(false) 
              .OnDelete(DeleteBehavior.Restrict); 

    });
}
    
    
}
}