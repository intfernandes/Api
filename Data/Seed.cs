using Microsoft.EntityFrameworkCore;
using Api.Entities;
using Api.Entities.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq; // Make sure System.Linq is included for Any()

namespace Api.Data
{
    public static class SeedDatabase
    {
        // --- GuidGenerator Class ---
        // Defines a helper class to generate sequential GUIDs for seed data.
        public static class GuidGenerator
        {
            private static int _counter = 1; // Static counter to ensure sequential GUIDs

            public static Guid GenerateSequentialGuid()
            {
                string hexValue = _counter.ToString("X8"); // Convert counter to 8-digit hexadecimal string
                string paddedHexValue = hexValue.PadLeft(12, '0'); // Pad with leading zeros to ensure 12 digits (hex)
                string guidString = $"00000000-0000-0000-0000-{paddedHexValue}"; // Use padded hex value in the last 12 digits
                _counter++;
                return Guid.Parse(guidString);
            }

            public static void ResetCounter()
            {
                _counter = 1; // Reset counter if you need to regenerate from the beginning
            }
        }

        // --- Initialize Method ---
        // Initializes the database with seed data if in development environment and database is empty.
        public static void Initialize(IServiceProvider serviceProvider, IHostEnvironment env)
        {
            using (var context = new DataContext( // Use your actual DataContext class name
                serviceProvider.GetRequiredService<DbContextOptions<DataContext>>())) // Use your actual DataContext class name
            {
                // Ensure this runs only in development
                if (!env.IsDevelopment()) return;

                GuidGenerator.ResetCounter(); // Reset GUID counter at the beginning of seeding

                // --- Seed Addresses ---
                // Seeds Address entities if the Addresses table is empty.
                if (!context.Addresses.Any())
                {
                    context.Addresses.AddRange(
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Street = "123 Main St", City = "Anytown", State = "CA", ZipCode = "12345" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Street = "456 Oak Ave", City = "Springfield", State = "IL", ZipCode = "67890" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Street = "789 Pine Ln", City = "Hill Valley", State = "NY", ZipCode = "10001" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Street = "10 Downing St", City = "London", State = "LDN", ZipCode = "SW1A 2AA" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Street = "5th Avenue", City = "New York", State = "NY", ZipCode = "10022" }
                    );
                    context.SaveChanges(); // Save Addresses
                }

                // --- Seed Categories ---
                // Seeds Category entities if the Categories table is empty.
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                        new Category { Id = GuidGenerator.GenerateSequentialGuid(), Name = "Electronics", Description = "Electronic gadgets and devices" },
                        new Category { Id = GuidGenerator.GenerateSequentialGuid(), Name = "Books", Description = "A wide range of books" },
                        new Category { Id = GuidGenerator.GenerateSequentialGuid(), Name = "Clothing", Description = "Fashionable clothing for all" }
                    );
                    context.SaveChanges(); // Save Categories
                }

                // --- Seed Domain Accounts ---
                // Seeds Account entities of type Domain if no Domain Accounts exist.
                if (!context.Accounts.OfType<Account>().Any(a => a.AccountType == AccountType.Domain)) // Check for Domain Accounts
                {
                    context.Accounts.AddRange(
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Domain, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Domain, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Domain, AccountStatus = AccountStatus.Inactive },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Domain, AccountStatus = AccountStatus.Pending },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Domain, AccountStatus = AccountStatus.Active }
                    );
                    context.SaveChanges(); // Save Domain Accounts
                }

                // --- Seed Domains ---
                // Seeds Domain entities if the Domains table is empty, using existing Domain Accounts and Addresses.
                if (!context.Domains.Any())
                {
                    // Fetch required entities from context
                    var DomainAccounts = context.Accounts.OfType<Account>().Where(a => a.AccountType == AccountType.Domain).ToList();
                    var addresses = context.Addresses.ToList(); // Fetch addresses

                    context.Domains.AddRange(
                        new Domain { Id = GuidGenerator.GenerateSequentialGuid(), AccountId = DomainAccounts[0].Id, AddressId = addresses[0].Id, Email = "admin@techcorp.com", Name = "TechCorp Inc.", Description = "Leading technology solutions provider" },
                        new Domain { Id = GuidGenerator.GenerateSequentialGuid(), AccountId = DomainAccounts[1].Id, AddressId = addresses[1].Id, Email = "admin@globalbooks.com", Name = "Global Books Ltd.", Description = "Worldwide book retailer" },
                        new Domain { Id = GuidGenerator.GenerateSequentialGuid(), AccountId = DomainAccounts[2].Id, AddressId = addresses[2].Id, Email = "admin@fashionhub.com", Name = "Fashion Hub", Description = "Trendy clothing and accessories" },
                        new Domain { Id = GuidGenerator.GenerateSequentialGuid(), AccountId = DomainAccounts[3].Id, AddressId = addresses[3].Id, Email = "admin@londonimports.com", Name = "London Imports", Description = "Importer of fine goods" },
                        new Domain { Id = GuidGenerator.GenerateSequentialGuid(), AccountId = DomainAccounts[4].Id, AddressId = addresses[4].Id, Email = "admin@citystyle.com", Name = "City Style Apparel", Description = "Urban fashion for everyone" }
                    );
                    context.SaveChanges(); // Save Domains
                }

                // --- Seed Customer Accounts ---
                // Seeds Account entities of type Customer if no Customer Accounts exist.
                if (!context.Accounts.OfType<Account>().Any(a => a.AccountType == AccountType.Customer)) // Check for Customer Accounts
                {
                    context.Accounts.AddRange(
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Customer, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Customer, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Customer, AccountStatus = AccountStatus.Inactive },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Customer, AccountStatus = AccountStatus.Pending },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Customer, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Customer, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Customer, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Customer, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Customer, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Customer, AccountStatus = AccountStatus.Active }
                    );
                    context.SaveChanges(); // Save Customer Accounts
                }

                // --- Seed Customers ---
                // Seeds Customer entities if the Customers table is empty, using existing Customer Accounts.
                if (!context.Customers.Any())
                {
                    var customerAccounts = context.Accounts.OfType<Account>().Where(a => a.AccountType == AccountType.Customer).ToList();

                    context.Customers.AddRange(
                        // Customer 1 ... Customer 10 (example Customers)
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Accounts = [customerAccounts[0]], FirstName = "Alice", LastName = "Smith", Email = "alice.smith@email.com", PhoneNumber = "555-1234" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Accounts = [customerAccounts[1]], FirstName = "Bob", LastName = "Johnson", Email = "bob.johnson@workmail.net", PhoneNumber = "555-5678" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Accounts = [customerAccounts[2]], FirstName = "Charlie", LastName = "Brown", Email = "charlie.b@someisp.org", PhoneNumber = "555-9012" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Accounts = [customerAccounts[3]], FirstName = "Diana", LastName = "Davis", Email = "diana.davis77@mail.co", PhoneNumber = "555-3456" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Accounts = [customerAccounts[4]], FirstName = "Ethan", LastName = "Miller", Email = "e.miller@fastmail.com", PhoneNumber = "555-7890" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Accounts = [customerAccounts[5]], FirstName = "Sophia", LastName = "Rodriguez", Email = "sophia.r@emailprovider.info", PhoneNumber = "555-1122" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Accounts = [customerAccounts[6]], FirstName = "Liam", LastName = "O'Connell", Email = "liam.o.c@webmail.me", PhoneNumber = "555-3344" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Accounts = [customerAccounts[7]], FirstName = "Olivia", LastName = "Wilson", Email = "olivia.wilson.online@email.net", PhoneNumber = "555-5566" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Accounts = [customerAccounts[8]], FirstName = "Jackson", LastName = "Lee", Email = "jackson.lee.jr@myemail.com", PhoneNumber = "555-7788" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Accounts = [customerAccounts[9]], FirstName = "Ava", LastName = "Hall", Email = "ava.hall.1990@email-service.com", PhoneNumber = "555-9900" }
                    );
                    context.SaveChanges(); // Save Customers
                }

                // --- Seed Member Accounts ---
                // Seeds Account entities of type Staff (Member Accounts) if no Member Accounts exist.
                if (!context.Accounts.OfType<Account>().Any(a => a.AccountType == AccountType.Staff)) // Check for Member Accounts (Staff type)
                {
                    context.Accounts.AddRange(
                         new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Staff, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Staff, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Staff, AccountStatus = AccountStatus.Inactive },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Staff, AccountStatus = AccountStatus.Pending },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Staff, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Staff, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Staff, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Staff, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Staff, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Staff, AccountStatus = AccountStatus.Active }
                    );
                    context.SaveChanges(); // Save Member Accounts
                }

                // --- Seed Members ---
                // Seeds Member entities if the Members table is empty, using existing Member Accounts and Domains.
                if (!context.Members.Any())
                {
                    var memberAccounts = context.Accounts.OfType<Account>().Where(a => a.AccountType == AccountType.Staff).ToList();
                    var Domains = context.Domains.ToList();

                    context.Members.AddRange(
                        // Member 1 ... Member 10 (example Members)
                        new Member { Id = GuidGenerator.GenerateSequentialGuid(), DomainId = Domains[0].Id, Accounts = [memberAccounts[0]], FirstName = "Fiona", LastName = "Green", Email = "fiona.green@Domain1.com", PhoneNumber = "555-4321" },
                        new Member { Id = GuidGenerator.GenerateSequentialGuid(), DomainId = Domains[0].Id, Accounts = [memberAccounts[1]], FirstName = "George", LastName = "Harris", Email = "george.harris@Domain1.com", PhoneNumber = "555-8765" },
                        new Member { Id = GuidGenerator.GenerateSequentialGuid(), DomainId = Domains[1].Id, Accounts = [memberAccounts[2]], FirstName = "Hannah", LastName = "Indigo", Email = "hannah.indigo@Domain2.com", PhoneNumber = "555-2109" },
                        new Member { Id = GuidGenerator.GenerateSequentialGuid(), DomainId = Domains[1].Id, Accounts = [memberAccounts[3]], FirstName = "Ian", LastName = "Jones", Email = "ian.jones@Domain2.com", PhoneNumber = "555-6543" },
                        new Member { Id = GuidGenerator.GenerateSequentialGuid(), DomainId = Domains[2].Id, Accounts = [memberAccounts[4]], FirstName = "Jack", LastName = "King", Email = "jack.king@Domain3.com", PhoneNumber = "555-0987" },
                        new Member { Id = GuidGenerator.GenerateSequentialGuid(), DomainId = Domains[2].Id, Accounts = [memberAccounts[5]], FirstName = "Katie", LastName = "Lopez", Email = "katie.lopez@Domain3.com", PhoneNumber = "555-2233" },
                        new Member { Id = GuidGenerator.GenerateSequentialGuid(), DomainId = Domains[3].Id, Accounts = [memberAccounts[6]], FirstName = "Mason", LastName = "Nguyen", Email = "mason.nguyen@Domain4.com", PhoneNumber = "555-4455" },
                        new Member { Id = GuidGenerator.GenerateSequentialGuid(), DomainId = Domains[3].Id, Accounts = [memberAccounts[7]], FirstName = "Nora", LastName = "Perez", Email = "nora.perez@Domain4.com", PhoneNumber = "555-6677" },
                        new Member { Id = GuidGenerator.GenerateSequentialGuid(), DomainId = Domains[4].Id, Accounts = [memberAccounts[8]], FirstName = "Owen", LastName = "Baker", Email = "owen.baker@Domain5.com", PhoneNumber = "555-8899" },
                        new Member { Id = GuidGenerator.GenerateSequentialGuid(), DomainId = Domains[4].Id, Accounts = [memberAccounts[9]], FirstName = "Penelope", LastName = "Carter", Email = "penelope.carter@Domain5.com", PhoneNumber = "555-0011" }
                    );
                    context.SaveChanges(); // Save Members
                }

                // --- Seed Products ---
                // Seeds Product entities if the Products table is empty, using existing Categories.
                if (!context.Products.Any())
                {
                    var categories = context.Categories.ToList();

                    context.Products.AddRange(
                        new Product { Id = GuidGenerator.GenerateSequentialGuid(), DomainId = null, Name = "Laptop", Description = "High-performance laptop", Price = 1200.00m, Categories = new List<Category> { categories.FirstOrDefault(c => c.Name == "Electronics")! } },
                        new Product { Id = GuidGenerator.GenerateSequentialGuid(), DomainId = null, Name = "Smartphone", Description = "Latest smartphone model", Price = 900.00m, Categories = new List<Category> { categories.FirstOrDefault(c => c.Name == "Electronics")! } },
                        new Product { Id = GuidGenerator.GenerateSequentialGuid(), DomainId = null, Name = "The Lord of the Rings", Description = "Fantasy classic", Price = 25.00m, Categories = new List<Category> { categories.FirstOrDefault(c => c.Name == "Books")! } }
                    );
                    context.SaveChanges(); // Save Products
                }

                 // --- Seed Photos ---
                // Seeds Photo entities if the Photos table is empty, linking to existing Products and Domains.
                if (!context.Photos.Any())
                {
                    var products = context.Products.ToList();
                    var Domains = context.Domains.ToList();

                    context.Photos.AddRange(
                        // Product Photos (example photos for Laptop and Smartphone products)
                        new Photo { Id = GuidGenerator.GenerateSequentialGuid(), ProductId = products[0].Id, ImageUrl = "https://example.com/laptop1.jpg", Description = "Laptop Product Photo 1" },
                        new Photo { Id = GuidGenerator.GenerateSequentialGuid(), ProductId = products[0].Id, ImageUrl = "https://example.com/laptop2.jpg", Description = "Laptop Product Photo 2" },
                        new Photo { Id = GuidGenerator.GenerateSequentialGuid(), ProductId = products[1].Id, ImageUrl = "https://example.com/smartphone1.jpg", Description = "Smartphone Product Photo 1" },
                        // Domain Logos/Showcase (example logos for TechCorp and Global Books)
                        new Photo { Id = GuidGenerator.GenerateSequentialGuid(), DomainId = Domains[0].Id, ImageUrl = "https://example.com/techcorp_logo.png", Description = "TechCorp Inc. Logo" },
                        new Photo { Id = GuidGenerator.GenerateSequentialGuid(), DomainId = Domains[1].Id, ImageUrl = "https://example.com/globalbooks_showroom.jpg", Description = "Global Books Showroom" }
                    );
                    context.SaveChanges(); // Save Photos
                }

                // --- Seed Orders and OrderItems ---
                // Seeds Order and OrderItem entities if the Orders table is empty, linking to existing Customers, Members, Domains, and Products.
                if (!context.Orders.Any())
                {
                    var customers = context.Customers.ToList();
                    var members = context.Members.ToList();
                    var Domains = context.Domains.ToList();
                    var products = context.Products.ToList();

                    // --- Order 1 ---
                    var order1Id = GuidGenerator.GenerateSequentialGuid();
                    context.Orders.Add(new Order  // Add Order entity to context
                    {
                        Id = order1Id,
                        CustomerId = customers[0].Id,
                        MemberId = members[0].Id,
                        DomainId = Domains[0].Id,
                        DeliveryTerm = DateTime.UtcNow.AddDays(3),
                        Status = OrderStatus.Completed
                    });
                    // Order 1 - Order Items
                    context.OrderItems.AddRange( // Add OrderItems for Order 1 to context
                         new OrderItem { Id = GuidGenerator.GenerateSequentialGuid(), OrderId = order1Id, ProductId = products[0].Id, Quantity = 1, Price = products[0].Price },
                         new OrderItem { Id = GuidGenerator.GenerateSequentialGuid(), OrderId = order1Id, ProductId = products[1].Id, Quantity = 2, Price = products[1].Price }
                    );
                    context.SaveChanges(); // Save Order 1 and its items to database

                    // --- Order 2 ---
                    var order2Id = GuidGenerator.GenerateSequentialGuid();
                    context.Orders.Add(new Order // Add Order entity to context
                    {
                        Id = order2Id,
                        CustomerId = customers[1].Id,
                        MemberId = members[1].Id,
                        DomainId = Domains[1].Id,
                        DeliveryTerm = DateTime.UtcNow.AddDays(5),
                        Status = OrderStatus.Shipped
                    });
                    // Order 2 - Order Items
                    context.OrderItems.AddRange( // Add OrderItems for Order 2 to context
                        new OrderItem { Id = GuidGenerator.GenerateSequentialGuid(), OrderId = order2Id, ProductId = products[2].Id, Quantity = 3, Price = products[2].Price }
                    );
                    context.SaveChanges(); // Save Order 2 and its items to database

                     // --- Order 3 ---
                    var order3Id = GuidGenerator.GenerateSequentialGuid();
                    context.Orders.Add(new Order
                    {
                        Id = order3Id,
                        CustomerId = customers[2].Id,
                        MemberId = members[2].Id,
                        DomainId = Domains[2].Id,
                        DeliveryTerm = DateTime.UtcNow.AddDays(2),
                        Status = OrderStatus.Processing
                    });
                    // Order 3 - Order Items
                    context.OrderItems.AddRange(
                        new OrderItem { Id = GuidGenerator.GenerateSequentialGuid(), OrderId = order3Id, ProductId = products[0].Id, Quantity = 2, Price = products[0].Price },
                        new OrderItem { Id = GuidGenerator.GenerateSequentialGuid(), OrderId = order3Id, ProductId = products[2].Id, Quantity = 1, Price = products[2].Price }
                    );
                    context.SaveChanges(); // Save Order 3 and its items

                    // --- Order 4 ---
                    var order4Id = GuidGenerator.GenerateSequentialGuid();
                    context.Orders.Add(new Order
                    {
                        Id = order4Id,
                        CustomerId = customers[3].Id,
                        MemberId = members[3].Id,
                        DomainId = Domains[3].Id,
                        DeliveryTerm = DateTime.UtcNow.AddDays(7),
                        Status = OrderStatus.Pending
                    });
                    // Order 4 - Order Items - No Order Items for Pending Order (example)
                    context.SaveChanges(); // Save Order 4

                    // --- Order 5 ---
                    var order5Id = GuidGenerator.GenerateSequentialGuid();
                    context.Orders.Add(new Order
                    {
                        Id = order5Id,
                        CustomerId = customers[4].Id,
                        MemberId = members[4].Id,
                        DomainId = Domains[4].Id,
                        DeliveryTerm = DateTime.UtcNow.AddDays(1), // Express
                        Status = OrderStatus.Completed
                    });
                    // Order 5 - Order Items
                    context.OrderItems.AddRange(
                        new OrderItem { Id = GuidGenerator.GenerateSequentialGuid(), OrderId = order5Id, ProductId = products[1].Id, Quantity = 1, Price = products[1].Price },
                        new OrderItem { Id = GuidGenerator.GenerateSequentialGuid(), OrderId = order5Id, ProductId = products[2].Id, Quantity = 2, Price = products[2].Price }
                    );
                    context.SaveChanges(); // Save Order 5 and its items

                    // --- Order 6 ---
                    var order6Id = GuidGenerator.GenerateSequentialGuid();
                    context.Orders.Add(new Order
                    {
                        Id = order6Id,
                        CustomerId = customers[5].Id,
                        MemberId = members[5].Id,
                        DomainId = Domains[0].Id, // Domain 1 again
                        DeliveryTerm = DateTime.UtcNow.AddDays(4),
                        Status = OrderStatus.Shipped
                    });
                    // Order 6 - Order Items
                    context.OrderItems.AddRange(
                        new OrderItem { Id = GuidGenerator.GenerateSequentialGuid(), OrderId = order6Id, ProductId = products[0].Id, Quantity = 1, Price = products[0].Price }
                    );
                    context.SaveChanges(); // Save Order 6 and its items

                    // --- Order 7 ---
                    var order7Id = GuidGenerator.GenerateSequentialGuid();
                    context.Orders.Add(new Order
                    {
                        Id = order7Id,
                        CustomerId = customers[6].Id,
                        MemberId = members[6].Id,
                        DomainId = Domains[2].Id, // Domain 3 again
                        DeliveryTerm = DateTime.UtcNow.AddDays(6),
                        Status = OrderStatus.Completed
                    });
                    // Order 7 - Order Items
                    context.OrderItems.AddRange(
                        new OrderItem { Id = GuidGenerator.GenerateSequentialGuid(), OrderId = order7Id, ProductId = products[1].Id, Quantity = 2, Price = products[1].Price }
                    );
                    context.SaveChanges(); // Save Order 7 and its items

                    // --- Order 8 ---
                    var order8Id = GuidGenerator.GenerateSequentialGuid();
                    context.Orders.Add(new Order
                    {
                        Id = order8Id,
                        CustomerId = customers[7].Id,
                        MemberId = members[7].Id,
                        DomainId = Domains[4].Id, // Domain 5 again
                        DeliveryTerm = DateTime.UtcNow.AddDays(2),
                        Status = OrderStatus.Processing
                    });
                    // Order 8 - Order Items
                    context.OrderItems.AddRange(
                        new OrderItem { Id = GuidGenerator.GenerateSequentialGuid(), OrderId = order8Id, ProductId = products[2].Id, Quantity = 1, Price = products[2].Price }
                    );
                    context.SaveChanges(); // Save Order 8 and its items
                }

                // ... (Seed Photos, OrderItems, ProductCategories, etc. further if needed) ...

            }
        }
    }
}