
using Api.Data;
using Api.Interfaces;
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
                
                services.AddScoped<ITokenService, TokenService>();
                
                return services;

        }
    }
}