using Microsoft.EntityFrameworkCore;
using Api.Entities;


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

                Console.WriteLine("Seeding database...");

                GuidGenerator.ResetCounter(); // Reset GUID counter at the beginning of seeding

                // --- Seed Addresses ---
                // Seeds Address entities if the Addresses table is empty.
                if (!context.Addresses.Any())
                {
                    context.Addresses.AddRange(
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Country = "USA", Square = "321", Number = "7",  Street = "123 Main St", City = "Anytown", State = "CA", ZipCode = "12345" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Country = "USA", Square = "321", Number = "7",  Street = "456 Oak Ave", City = "Springfield", State = "IL", ZipCode = "67890" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Country = "USA", Square = "321", Number = "7",  Street = "789 Pine Ln", City = "Hill Valley", State = "NY", ZipCode = "10001" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Country = "USA", Square = "321", Number = "7",  Street = "10 Downing St", City = "London", State = "LDN", ZipCode = "SW1A 2AA" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Country = "USA", Square = "321", Number = "7",  Street = "5th Avenue", City = "New York", State = "NY", ZipCode = "10022" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Country = "USA", Square = "321", Number = "7",  Street = "Rue de Rivoli", City = "Paris", State = "IDF", ZipCode = "75001" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Country = "USA", Square = "321", Number = "7",  Street = "Alexanderplatz", City = "Berlin", State = "BE", ZipCode = "10178" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Country = "USA", Square = "321", Number = "7",  Street = "Piazza di Spagna", City = "Rome", State = "RM", ZipCode = "00187" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Country = "USA", Square = "321", Number = "7",  Street = "Shibuya Crossing", City = "Tokyo", State = "TKY", ZipCode = "150-8010" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Country = "USA", Square = "321", Number = "7",  Street = "Sydney Opera House", City = "Sydney", State = "NSW", ZipCode = "2000" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Country = "USA", Square = "321", Number = "7",  Street = "Kremlin", City = "Moscow", State = "MOW", ZipCode = "103132" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Country = "USA", Square = "321", Number = "7",  Street = "Christ the Redeemer", City = "Rio de Janeiro", State = "RJ", ZipCode = "22250-040" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Country = "USA", Square = "321", Number = "7",  Street = "Table Mountain", City = "Cape Town", State = "WC", ZipCode = "8001" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Country = "USA", Square = "321", Number = "7",  Street = "Uluru", City = "Yulara", State = "NT", ZipCode = "0872" },
                        new Address { Id = GuidGenerator.GenerateSequentialGuid(), Country = "USA", Square = "321", Number = "7",  Street = "Chichen Itza", City = "Yucatán", State = "YUC", ZipCode = "97751" }
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
                        new Category { Id = GuidGenerator.GenerateSequentialGuid(), Name = "Clothing", Description = "Fashionable clothing for all" },
                        new Category { Id = GuidGenerator.GenerateSequentialGuid(), Name = "Home", Description = "Home decor and furnishings" },
                        new Category { Id = GuidGenerator.GenerateSequentialGuid(), Name = "Toys", Description = "Fun and educational toys" }
                    );
                    context.SaveChanges(); // Save Categories
                }

                // --- Seed Store Accounts ---
                // Seeds Account entities of type Store if no Store Accounts exist.
                if (!context.Accounts.OfType<Account>().Any(a => a.AccountType == AccountType.Store)) // Check for Store Accounts
                {
                    context.Accounts.AddRange(
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Store, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Store, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Store, AccountStatus = AccountStatus.Inactive },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Store, AccountStatus = AccountStatus.Pending },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Store, AccountStatus = AccountStatus.Active }
                    );
                    context.SaveChanges(); // Save Store Accounts
                }

                // --- Seed Stores ---
                // Seeds Store entities if the Stores table is empty, using existing Store Accounts and Addresses.
                if (!context.Stores.Any())
                {
                    // Fetch required entities from context
                    var storeAccounts = context.Accounts.OfType<Account>().Where(a => a.AccountType == AccountType.Store).ToList();
                    var addresses = context.Addresses.ToList();

                    context.Stores.AddRange(
                        new Store { Id = GuidGenerator.GenerateSequentialGuid(), AccountId = storeAccounts[0].Id, AddressId = addresses[0].Id, Email = "admin@techcorp.com", Name = "TechCorp Inc.", Description = "Leading technology solutions provider" },
                        new Store { Id = GuidGenerator.GenerateSequentialGuid(), AccountId = storeAccounts[1].Id, AddressId = addresses[1].Id, Email = "admin@globalbooks.com", Name = "Global Books Ltd.", Description = "Worldwide book retailer" },
                        new Store { Id = GuidGenerator.GenerateSequentialGuid(), AccountId = storeAccounts[2].Id, AddressId = addresses[2].Id, Email = "admin@fashionhub.com", Name = "Fashion Hub", Description = "Trendy clothing and accessories" },
                        new Store { Id = GuidGenerator.GenerateSequentialGuid(), AccountId = storeAccounts[3].Id, AddressId = addresses[3].Id, Email = "admin@londonimports.com", Name = "London Imports", Description = "Importer of fine goods" },
                        new Store { Id = GuidGenerator.GenerateSequentialGuid(), AccountId = storeAccounts[4].Id, AddressId = addresses[4].Id, Email = "admin@citystyle.com", Name = "City Style Apparel", Description = "Urban fashion for everyone" }
                    );
                    context.SaveChanges(); // Save Stores
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
                    var addresses = context.Addresses.ToList();


                    context.Customers.AddRange(
                        // Customer 1 ... Customer 10 (example Customers)
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[0],  Accounts = [customerAccounts[0]], FirstName = "Alice", LastName = "Smith", Email = "alice.smith@email.com", PhoneNumber = "555-1234" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[1],  Accounts = [customerAccounts[1]], FirstName = "Bob", LastName = "Johnson", Email = "bob.johnson@workmail.net", PhoneNumber = "555-5678" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[2],  Accounts = [customerAccounts[2]], FirstName = "Charlie", LastName = "Brown", Email = "charlie.b@someisp.org", PhoneNumber = "555-9012" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[3],  Accounts = [customerAccounts[3]], FirstName = "Diana", LastName = "Davis", Email = "diana.davis77@mail.co", PhoneNumber = "555-3456" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[4],  Accounts = [customerAccounts[4]], FirstName = "Ethan", LastName = "Miller", Email = "e.miller@fastmail.com", PhoneNumber = "555-7890" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[5],  Accounts = [customerAccounts[5]], FirstName = "Sophia", LastName = "Rodriguez", Email = "sophia.r@emailprovider.info", PhoneNumber = "555-1122" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[6],  Accounts = [customerAccounts[6]], FirstName = "Liam", LastName = "O'Connell", Email = "liam.o.c@webmail.me", PhoneNumber = "555-3344" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[7],  Accounts = [customerAccounts[7]], FirstName = "Olivia", LastName = "Wilson", Email = "olivia.wilson.online@email.net", PhoneNumber = "555-5566" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[8],  Accounts = [customerAccounts[8]], FirstName = "Jackson", LastName = "Lee", Email = "jackson.lee.jr@myemail.com", PhoneNumber = "555-7788" },
                        new Customer { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[9],  Accounts = [customerAccounts[9]], FirstName = "Ava", LastName = "Hall", Email = "ava.hall.1990@email-service.com", PhoneNumber = "555-9900" }
                    );
                    context.SaveChanges(); // Save Customers
                }

                // --- Seed Employee Accounts ---
                // Seeds Account entities of type Staff (Employee Accounts) if no Employee Accounts exist.
                if (!context.Accounts.OfType<Account>().Any(a => a.AccountType == AccountType.Staff || a.AccountType == AccountType.Admin || a.AccountType == AccountType.Manager  )) // Check for Employee Accounts (Staff/Admin/Manager type)
                {
                    context.Accounts.AddRange(
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Admin, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Manager, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Staff, AccountStatus = AccountStatus.Inactive },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Admin, AccountStatus = AccountStatus.Pending },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Manager, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Staff, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Admin, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Manager, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Staff, AccountStatus = AccountStatus.Active },
                        new Account { Id = GuidGenerator.GenerateSequentialGuid(), AccountType = AccountType.Staff, AccountStatus = AccountStatus.Active }
                    );
                    context.SaveChanges(); // Save Employee Accounts
                }

                // --- Seed Employees ---
                // Seeds Employee entities if the Employees table is empty, using existing Employee Accounts and Stores.
                if (!context.Employees.Any())
                {
                    var employeeAccounts = context.Accounts.OfType<Account>().Where(a => a.AccountType == AccountType.Staff || a.AccountType == AccountType.Admin || a.AccountType == AccountType.Manager).ToList();
                    var stores = context.Stores.ToList();
                    var addresses = context.Addresses.ToList();


                    context.Employees.AddRange(
                        // Employee 1 ... Employee 10 (example Employees)
                        new Employee { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[10], StoreId = stores[0].Id, Accounts = [employeeAccounts[0]], FirstName = "Fiona", LastName = "Green", Email = "fiona.green@Store1.com", PhoneNumber = "555-4321" },
                        new Employee { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[11], StoreId = stores[0].Id, Accounts = [employeeAccounts[1]], FirstName = "George", LastName = "Harris", Email = "george.harris@Store1.com", PhoneNumber = "555-8765" },
                        new Employee { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[12], StoreId = stores[1].Id, Accounts = [employeeAccounts[2]], FirstName = "Hannah", LastName = "Indigo", Email = "hannah.indigo@Store2.com", PhoneNumber = "555-2109" },
                        new Employee { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[13], StoreId = stores[1].Id, Accounts = [employeeAccounts[3]], FirstName = "Ian", LastName = "Jones", Email = "ian.jones@Store2.com", PhoneNumber = "555-6543" },
                        new Employee { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[14], StoreId = stores[2].Id, Accounts = [employeeAccounts[4]], FirstName = "Jack", LastName = "King", Email = "jack.king@Store3.com", PhoneNumber = "555-0987" },
                        new Employee { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[5], StoreId = stores[2].Id, Accounts = [employeeAccounts[5]], FirstName = "Katie", LastName = "Lopez", Email = "katie.lopez@Store3.com", PhoneNumber = "555-2233" },
                        new Employee { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[6], StoreId = stores[3].Id, Accounts = [employeeAccounts[6]], FirstName = "Mason", LastName = "Nguyen", Email = "mason.nguyen@Store4.com", PhoneNumber = "555-4455" },
                        new Employee { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[7], StoreId = stores[3].Id, Accounts = [employeeAccounts[7]], FirstName = "Nora", LastName = "Perez", Email = "nora.perez@Store4.com", PhoneNumber = "555-6677" },
                        new Employee { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[8], StoreId = stores[4].Id, Accounts = [employeeAccounts[8]], FirstName = "Owen", LastName = "Baker", Email = "owen.baker@Store5.com", PhoneNumber = "555-8899" },
                        new Employee { Id = GuidGenerator.GenerateSequentialGuid(), Address = addresses[9], StoreId = stores[4].Id, Accounts = [employeeAccounts[9]], FirstName = "Penelope", LastName = "Carter", Email = "penelope.carter@Store5.com", PhoneNumber = "555-0011" }
                    );
                    context.SaveChanges(); // Save Employees
                }

                // --- Seed Products ---
                // Seeds Product entities if the Products table is empty, using existing Categories.
                if (!context.Products.Any())
                {
                    var categories = context.Categories.ToList();
                    var stores = context.Stores.ToList();

                    context.Products.AddRange(
                        // Product 1 ... Product 10 (example Products)
                        new Product { Id = GuidGenerator.GenerateSequentialGuid(), StoreId = stores[0].Id, Name = "Laptop", Description = "High-performance laptop", Price = 999.99m, Categories = [categories[0]] },
                        new Product { Id = GuidGenerator.GenerateSequentialGuid(), StoreId = stores[0].Id, Name = "Tablet", Description = "Portable tablet device", Price = 299.99m, Categories = [categories[0]] },
                        new Product { Id = GuidGenerator.GenerateSequentialGuid(), StoreId = stores[1].Id, Name = "Smartphone", Description = "Latest smartphone model", Price = 699.99m, Categories = [categories[0]] },
                        new Product { Id = GuidGenerator.GenerateSequentialGuid(), StoreId = stores[1].Id, Name = "E-Reader", Description = "E-ink reader for books", Price = 149.99m, Categories = [categories[1]] },
                        new Product { Id = GuidGenerator.GenerateSequentialGuid(), StoreId = stores[2].Id, Name = "T-Shirt", Description = "Casual cotton t-shirt", Price = 19.99m, Categories = [categories[2]] },
                        new Product { Id = GuidGenerator.GenerateSequentialGuid(), StoreId = stores[2].Id, Name = "Jeans", Description = "Denim jeans for all", Price = 39.99m, Categories = [categories[2]] },
                        new Product { Id = GuidGenerator.GenerateSequentialGuid(), StoreId = stores[3].Id, Name = "Dress", Description = "Elegant evening dress", Price = 79.99m, Categories = [categories[2]] },
                        new Product { Id = GuidGenerator.GenerateSequentialGuid(), StoreId = stores[3].Id, Name = "Suit", Description = "Formal business suit", Price = 149.99m, Categories = [categories[2]] },
                        new Product { Id = GuidGenerator.GenerateSequentialGuid(), StoreId = stores[4].Id, Name = "Sneakers", Description = "Sporty sneakers for all", Price = 59.99m, Categories = [categories[2]] },
                        new Product { Id = GuidGenerator.GenerateSequentialGuid(), StoreId = stores[4].Id, Name = "Gloves", Description = "Warm up your hands, soft touch", Price = 9.99m, Categories = [categories[2]] }

              );
                    context.SaveChanges(); // Save Products
                }

                 // --- Seed Photos ---
                // Seeds Photo entities if the Photos table is empty, linking to existing Products and Stores.
                if (!context.Photos.Any())
                {
                    var employees = context.Employees.ToList();
                    var customers = context.Customers.ToList();
                    var products = context.Products.ToList(); 
                    var stores = context.Stores.ToList();
                    var categories = context.Categories.ToList();

                    for (int i = 0; i < 40; i++)
                    {
                        if(i >= 0 && i < 10){
                            var employee = employees[i];
                            var photo = new Photo { Id = GuidGenerator.GenerateSequentialGuid(), Highlight = true, EmployeeId = employee.Id, ImageUrl = $"https://randomuser.me/api/portraits/women/{i}.jpg", Description = $"Profile Photo for Employee {employees[i].Id}" };
                            context.Photos.Add(photo);
                            employee.Photos = [photo];

                            continue;
                        }
                        if(i >= 10 && i < 20){
                            var customer = customers[i-10];
                            var photo =  new Photo { Id = GuidGenerator.GenerateSequentialGuid(), Highlight = true, CustomerId = customers[i-10].Id, ImageUrl = $"https://randomuser.me/api/portraits/men/{i}.jpg", Description = $"Profile Photo for Customer {customers[i-10].Id}" };
                            context.Photos.Add(photo);
                            customer.Photos = [photo];

                            continue;
                        }
                        if(i >= 20 && i < 30) {
                            var product = products[i-20];
                            var photo =    new Photo { Id = GuidGenerator.GenerateSequentialGuid(), Highlight = true, ProductId = products[i-20].Id, ImageUrl = $"https://randomuser.me/api/portraits/men/{i}.jpg", Description = $"Product Photo for Product {products[i-20].Id}" };
                            context.Photos.Add(photo); 
                            product.Photos = [photo];
                            continue;
                        }

                          if(i >= 30 && i < 35){
                            var store = stores[i-30];
                            var photo = new Photo { Id = GuidGenerator.GenerateSequentialGuid(), Highlight = true, StoreId = stores[i-30].Id, ImageUrl = $"https://randomuser.me/api/portraits/men/{i}.jpg", Description = $"Product Photo for Product {products[i-30].Id}" };               
                            context.Photos.Add(photo);
                            store.Photos = [photo];
                            continue;
                        }
                    
                           if(i >= 35 && i < 40){
                            var category = categories[i-35];
                            var photo = new Photo { Id = GuidGenerator.GenerateSequentialGuid(), Highlight = true, StoreId = stores[i-35].Id, ImageUrl = $"https://randomuser.me/api/portraits/men/{i+35}.jpg", Description = $"Product Photo for Product {products[i-35].Id}" };               
                            context.Photos.Add(photo);
                            category.Photos = [photo];
                            continue;
                        }

                        
                    }
                    context.SaveChanges(); // Save Photos
                }

                // --- Seed Orders and OrderItems ---
                // Seeds Order and OrderItem entities if the Orders table is empty, linking to existing Customers, Employees, Stores, and Products.
                if (!context.Orders.Any())
                {
                    var customers = context.Customers.ToList();
                    var employees = context.Employees.ToList();
                    var stores = context.Stores.ToList();
                    var products = context.Products.ToList();

                    // --- Order 1 ---
                    var order1Id = GuidGenerator.GenerateSequentialGuid();
                    context.Orders.Add(new Order  // Add Order entity to context
                    {
                        Id = order1Id,
                        CustomerId = customers[0].Id,
                        EmployeeId = employees[0].Id,
                        StoreId = stores[0].Id,
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
                        EmployeeId = employees[1].Id,
                        StoreId = stores[1].Id,
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
                        EmployeeId = employees[2].Id,
                        StoreId = stores[2].Id,
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
                        EmployeeId = employees[3].Id,
                        StoreId = stores[3].Id,
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
                        EmployeeId = employees[4].Id,
                        StoreId = stores[4].Id,
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
                        EmployeeId = employees[5].Id,
                        StoreId = stores[0].Id, // Store 1 again
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
                        EmployeeId = employees[6].Id,
                        StoreId = stores[2].Id, // Store 3 again
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
                        EmployeeId = employees[7].Id,
                        StoreId = stores[4].Id, // Store 5 again
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

                Console.WriteLine("Seeding database completed.");


            }
        }
    }
}