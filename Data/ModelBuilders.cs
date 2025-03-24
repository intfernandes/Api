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

        entity.HasOne(e => e.Customer)
            .WithMany(u => u.Accounts)
            .HasForeignKey(e => e.CustomerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.Employee)
            .WithMany(u => u.Accounts)
            .HasForeignKey(e => e.EmployeeId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        // The Store - Account relationship remains unchanged:
        entity.HasOne(e => e.Store)
             .WithOne(c => c.Account)
             .HasForeignKey<Account>(e => e.StoreId)
             .OnDelete(DeleteBehavior.Restrict);
    });
}

public static void ConfigureCompanies(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Store>(entity =>
    {
            entity.ToTable("Stores");
 
            entity.HasMany(c => c.Employees)
              .WithOne(m => m.Store)
              .HasForeignKey(m => m.StoreId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);
  
            entity.HasMany(c => c.Products)
              .WithOne(p => p.Store)
              .HasForeignKey(p => p.StoreId)
              .OnDelete(DeleteBehavior.Cascade);
 
            entity.HasMany(c => c.Orders)
              .WithOne(o => o.Store)
              .HasForeignKey(o => o.StoreId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(c => c.Photos)    
              .WithOne(p => p.Store)     
              .HasForeignKey(p => p.StoreId) 
              .OnDelete(DeleteBehavior.Cascade);
    });
}

public static void ConfigureUsers(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Employee>(entity =>
    {
            entity.ToTable("Employees");
        
            entity.HasKey(a => a.Id);
        
            entity.Property(e => e.Gender)
              .HasConversion<string>()
              .HasColumnType("nvarchar(24)")
              .IsRequired(false);

            entity.HasOne(c => c.Store)      
              .WithMany(m => m.Employees)      
              .HasForeignKey(c => c.StoreId) 
              .IsRequired(false)                
              .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(c => c.Photos)    
              .WithOne(p => p.Employee)     
              .HasForeignKey(p => p.EmployeeId) 
              .OnDelete(DeleteBehavior.Cascade);
    });

        modelBuilder.Entity<Customer>(entity =>
    {
            entity.ToTable("Customers");
        
            entity.HasKey(a => a.Id);
        
            entity.Property(e => e.Gender)
              .HasConversion<string>()
              .HasColumnType("nvarchar(24)")
              .IsRequired(false);

            entity.HasMany(c => c.Photos)    
              .WithOne(p => p.Customer)     
              .HasForeignKey(p => p.CustomerId) 
              .OnDelete(DeleteBehavior.Cascade);
    });
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

      entity.HasOne(a => a.Store)      
              .WithOne(c => c.Address)      
              .HasForeignKey<Store>(c => c.AddressId) 
              .IsRequired(false)                
              .OnDelete(DeleteBehavior.Restrict);  

      entity.HasOne(a => a.Customer)        
              .WithOne(u => u.Address)       
              .HasForeignKey<Customer>(u => u.AddressId) 
              .IsRequired(false)                
              .OnDelete(DeleteBehavior.Restrict);

      entity.HasOne(a => a.Employee)
              .WithOne(u => u.Address)
              .HasForeignKey<Employee>(u => u.AddressId)
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
                                                 
            entity.HasOne(p => p.Store)      
              .WithMany(c => c.Products)        
              .HasForeignKey(p => p.StoreId)  
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

        entity.HasOne(o => o.Employee)
              .WithMany(u => u.Orders) 
              .HasForeignKey(o => o.EmployeeId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(o => o.Store)
              .WithMany(c => c.Orders)
              .HasForeignKey(o => o.StoreId)
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

        entity.Property(e => e.CustomerId)
              .IsRequired(false); 

        entity.HasOne(e => e.Customer)
              .WithMany()       
              .HasForeignKey(e => e.CustomerId) 
              .IsRequired(false) 
              .OnDelete(DeleteBehavior.Restrict); 

        entity.Property(e => e.EmployeeId)
              .IsRequired(false);

        entity.HasOne(e => e.Employee)
              .WithMany()       
              .HasForeignKey(e => e.EmployeeId) 
              .IsRequired(false) 
              .OnDelete(DeleteBehavior.Restrict);

    });
}
    
    
}
}