
using Api.Data;
using Api.Data.Repositories;
using Api.Interfaces;
using Api.Repositories;
using Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddAplicationServices(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment env) {
                
                services.AddControllers();
                
                if(env.IsDevelopment() ){ 
                    // set db enviroment to development (sqlite on ./constance.db)
                    services.AddDbContext<DataContext >(opt => {
                        opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
                        });

                    
                } else {
                    // set db enviroment to production
                    services.AddDbContext<DataContext >(opt => {
                        opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
                        });
                }
                
                services.AddCors();
                

                services.AddScoped<IAccountsRepository, AccountsRepository>(); 
                services.AddScoped<IAddressRepository, AddressRepository>();
                services.AddScoped<ICategoriesRepository, CategoryRepository>();
                services.AddScoped<ICustomersRepository, CustomersRepository>();
                services.AddScoped<IStoresRepository, StoresRepository>();
                services.AddScoped<IEmployeesRepository, EmployeesRepository>(); 
                services.AddScoped<IOrdersRepository, OrdersRepository>();
                services.AddScoped<IPhotosRepository, PhotoRepository>();
                services.AddScoped<IProductsRepository, ProductRepository>();
                services.AddScoped<ITokenService, TokenService>();
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                
                return services;

        }
    }
}