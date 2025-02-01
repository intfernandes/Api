
using Api.Entities;
using Api.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
        public class ModelBuilders
        {

       
              public static void ConfigureCompanies(ModelBuilder modelBuilder)
              {
                 modelBuilder.Entity<Company>(b =>
                 {

            modelBuilder.Entity<Company>().ToTable("Companies");
      
            b.HasOne(c => c.Account)
             .WithOne(a => a.Company)
             .HasForeignKey<Account>(a => a.CompanyId);  

            b.HasOne(u => u.Address)
             .WithOne(a => a.Company)
             .HasForeignKey<Address>(a => a.CompanyId)
             .OnDelete(DeleteBehavior.SetNull);

             b.HasMany(c => c.Members)
            .WithOne(m => m.Company)
            .HasForeignKey(m => m.CompanyId);

            b.HasMany(c => c.Products)
            .WithOne(p => p.Company)
            .HasForeignKey(p => p.CompanyId);

             b.HasMany(c => c.Orders)
            .WithOne(o => o.Company)
            .HasForeignKey(o => o.CompanyId);
        }); 
         
    }


    public static void ConfigureMembers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>(b =>
        {
            modelBuilder.Entity<Member>().ToTable("Members");

            b.HasOne(m => m.Company)
                .WithMany()
                .HasForeignKey(m => m.CompanyId )
                 .OnDelete(DeleteBehavior.Restrict); 

            b.HasMany(m => m.Accounts)
             .WithOne( )
             .HasForeignKey(a => a.MemberId)
             .OnDelete(DeleteBehavior.Restrict); 

            b.HasOne(m => m.Address)
             .WithOne()
             .HasForeignKey<Address>(a => a.MemberId)
             .OnDelete(DeleteBehavior.SetNull);

            b.HasMany(m => m.Photos)
             .WithOne()
             .HasForeignKey(p => p.MemberId)
             .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(u => u.HighlightPhoto)
                .WithOne()
                .HasForeignKey<Member>(u => u.HighlightPhotoId)
                .OnDelete(DeleteBehavior.SetNull);


        });
    }

   public static void ConfigureCustomer(ModelBuilder modelBuilder)
{
        modelBuilder.Entity<Customer>(b =>
        {
            modelBuilder.Entity<Customer>().ToTable("Customers"); 

            b.HasMany(u => u.Accounts)
             .WithOne(a => a.Customer ) 
             .HasForeignKey(a => a.CustomerId)
             .OnDelete(DeleteBehavior.Restrict); 

            b.HasOne(u => u.Address)
             .WithOne(a => a.Customer)
             .HasForeignKey<Address>(a => a.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);

            b.HasMany(u => u.Photos)
             .WithOne()
             .HasForeignKey(p => p.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(u => u.HighlightPhoto)
                .WithOne()
                .HasForeignKey<Customer>(u => u.HighlightPhotoId)
                .OnDelete(DeleteBehavior.SetNull);

              b.HasMany(c => c.PaymentMethods)
             .WithOne(p => p.Customer)
             .HasForeignKey(p => p.CustomerId);

            b.HasMany(c => c.Ratings)
             .WithOne(p => p.Customer)
             .HasForeignKey(p => p.CustomerId);

                b.HasMany(c => c.Carts)
             .WithOne(p => p.Customer)
             .HasForeignKey(p => p.CustomerId);

                b.HasMany(c => c.Wishlists)
             .WithOne(p => p.Customer)
             .HasForeignKey(p => p.CustomerId);

                b.HasMany(c => c.Subscriptions)
             .WithOne(p => p.Customer)
             .HasForeignKey(p => p.CustomerId);

                b.HasMany(c => c.Feedbacks)
             .WithOne(p => p.Customer)
             .HasForeignKey(p => p.CustomerId);

                b.HasMany(c => c.Complaints)
             .WithOne(p => p.Customer)
             .HasForeignKey(p => p.CustomerId);

                b.HasMany(c => c.Refunds)
             .WithOne(p => p.Customer)
             .HasForeignKey(p => p.CustomerId);

                b.HasMany(c => c.Returns)
             .WithOne(p => p.Customer)
             .HasForeignKey(p => p.CustomerId);

                b.HasMany(c => c.Reports)
             .WithOne(p => p.Customer)
             .HasForeignKey(p => p.CustomerId);

                b.HasMany(c => c.Chats)
             .WithOne(p => p.Customer)
             .HasForeignKey(p => p.CustomerId);

                b.HasMany(c => c.Conversations)
             .WithOne(p => p.Customer)
             .HasForeignKey(p => p.CustomerId);

                b.HasMany(c => c.Messages)
             .WithOne(p => p.Customer)
             .HasForeignKey(p => p.CustomerId);

                b.HasMany(c => c.Notifications)
             .WithOne(p => p.Customer)
             .HasForeignKey(p => p.CustomerId);

       

        });
}

    public static void ConfigureProducts(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(b =>
            { 
                b.HasOne(p => p.Company)
                .WithMany()
                .HasForeignKey(p => p.CompanyId);

                b.HasMany(p => p.Photos)
                    .WithOne()
                    .HasForeignKey(p => p.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(p => p.HighlightPhoto)
                    .WithOne()
                    .HasForeignKey<Photo>(p => p.Id); 


                    b.HasMany(p => p.Categories)
                    .WithMany(c => c.Products)
                    .UsingEntity<ProductCategory>(
                j => j.HasKey(pc => new { pc.ProductId , pc.CategoryId }));

                    
            
        }); 

    }



    public static void ConfigurePhoto(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Photo>(b =>
        {
            b.HasOne(p => p.Member)
                .WithMany(m => m.Photos)
                .HasForeignKey(p => p.MemberId)
                .OnDelete(DeleteBehavior.Cascade); 

                  b.HasOne(p => p.Customer)
                .WithMany(m => m.Photos)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); 

            b.HasOne(p => p.Product)
                .WithMany(p => p.Photos)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

        public static void ConfigureAdresses(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(b =>
        {
            b.HasKey(a => a.Id);

            b.HasOne(a => a.Company)
                .WithOne(u => u.Address )
                .HasForeignKey<Company>(a => a.Id);

            b.HasOne(a => a.Member)
                .WithOne(u => u.Address )
                .HasForeignKey<Member>(a => a.Id);

            b.HasOne(a => a.Customer)
                .WithOne(u => u.Address )
                .HasForeignKey<Customer>(a => a.Id);
        });
    }

      public static void ConfigureOrder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(b =>
    {

        b.HasKey(o => o.Id);

        b.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(o => o.Member)
            .WithMany(m => m.Orders)
            .HasForeignKey(o => o.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade); 

        b.Property(o => o.Status).HasConversion<string>(); 
    });

       modelBuilder.Entity<OrderItem>(b =>
    {
        b.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade); 

        b.HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict); 
    });
    }}
}