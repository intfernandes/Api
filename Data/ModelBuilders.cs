
using Api.Entities;
using Api.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
        public class ModelBuilders
        {

            public static void ConfigureAccounts(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Account>(b =>
                {
                    b.HasKey(a => a.Id);

                    b.HasOne(a => a.Member)
                        .WithMany()
                        .HasForeignKey(m => m.MemberId)
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne(a => a.Customer)
                        .WithMany()
                        .HasForeignKey(c => c.CustomerId)
                        .OnDelete(DeleteBehavior.Restrict);

             
               
      
                });
            }

              public static void ConfigureCompanies(ModelBuilder modelBuilder)
    {
        
                  modelBuilder.Entity<Company>(b =>
        {
            b.HasKey(c => c.Id);

            b.HasOne(c => c.Account)
             .WithOne(a => a.Company)
             .HasForeignKey<Account>(a => a.CompanyId);  

                    b.HasOne(u => u.Address)
             .WithOne()
             .HasForeignKey<Address>(p => p.CompanyId)
             .OnDelete(DeleteBehavior.SetNull);
        }); 


         
    }

    public static void ConfigureMembers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>(b =>
        {
            b.HasMany(u => u.Accounts)
             .WithOne()
             .HasForeignKey(u => u.MemberId)
             .OnDelete(DeleteBehavior.Restrict); 

                     b.HasOne(u => u.Address)
             .WithOne()
             .HasForeignKey<Address>(p => p.MemberId)
             .OnDelete(DeleteBehavior.SetNull);

            b.HasMany(u => u.Photos)
             .WithOne()
             .HasForeignKey(p => p.UserId)
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
            b.HasMany(u => u.Accounts)
             .WithOne() 
             .HasForeignKey(a => a.CustomerId)
             .OnDelete(DeleteBehavior.Restrict); 

               b.HasOne(u => u.Address)
             .WithOne()
             .HasForeignKey<Address>(p => p.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);

            b.HasMany(u => u.Photos)
             .WithOne()
             .HasForeignKey(p => p.UserId)
             .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(u => u.HighlightPhoto)
                .WithOne()
                .HasForeignKey<Customer>(u => u.HighlightPhotoId)
                .OnDelete(DeleteBehavior.SetNull);
 #region Relations 
                    b.HasOne(u => u.PaymentMethods)
             .WithOne()
             .HasForeignKey<PaymentMethod>(p => p.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);

                 b.HasOne(u => u.Reviews)
             .WithOne()
             .HasForeignKey<Review>(p => p.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);

                 b.HasOne(u => u.Ratings)
             .WithOne()
             .HasForeignKey<Rating>(p => p.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);

                 b.HasOne(u => u.Carts)
             .WithOne()
             .HasForeignKey<Cart>(p => p.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);

                 b.HasOne(u => u.Wishlists)
             .WithOne()
             .HasForeignKey<Wishlist>(p => p.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);

                 b.HasOne(u => u.Subscriptions)
             .WithOne()
             .HasForeignKey<Subscription>(p => p.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);

                 b.HasOne(u => u.Feedbacks)
             .WithOne()
             .HasForeignKey<Feedback>(p => p.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);

                 b.HasOne(u => u.Complaints)
             .WithOne()
             .HasForeignKey<Complaint>(p => p.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);

                 b.HasOne(u => u.Refunds)
             .WithOne()
             .HasForeignKey<Refund>(p => p.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);

                 b.HasOne(u => u.Returns)
             .WithOne()
             .HasForeignKey<Return>(p => p.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);

                 b.HasOne(u => u.Reports)
             .WithOne()
             .HasForeignKey<Report>(p => p.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);

                 b.HasOne(u => u.Chats)
             .WithOne()
             .HasForeignKey<Chat>(p => p.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);

                 b.HasOne(u => u.Conversations)
             .WithOne()
             .HasForeignKey<Conversation>(p => p.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);

                 b.HasOne(u => u.Messages)
             .WithOne()
             .HasForeignKey<Message>(p => p.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);

                 b.HasOne(u => u.Notifications)
             .WithOne()
             .HasForeignKey<Notification>(p => p.CustomerId)
             .OnDelete(DeleteBehavior.SetNull);



             #endregion

        });
}

    public static void ConfigureProducts(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(b =>
            { 
                b.HasMany(p => p.Photos)
                    .WithOne(p => p.Product)
                    .HasForeignKey(p => p.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasKey(p => p.Id);
                    b.HasMany(p => p.Categories)
                    .WithMany(c => c.Products)
                    .UsingEntity<ProductCategory>(
                j => j.HasKey(pc => new { pc.ProductId , pc.CategoryId }));

                    
            
        });
        modelBuilder.Entity<Category>(b =>{b.HasKey(c => c.Id);});

    }



    public static void ConfigurePhoto(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Photo>(b =>
        {
            b.HasOne(p => p.User)
                .WithMany(m => m.Photos)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); 

            b.HasOne(p => p.Product)
                .WithMany(p => p.Photos)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

      public static void ConfigureOrder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(b =>
    {
        b.HasOne(o => o.Customer)
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(o => o.Member)
            .WithMany()
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