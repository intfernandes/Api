using Api.Data;
using Api.Extensions;
using Api.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// add services to the container
builder.Services.AddAplicationServices(builder.Configuration, builder.Environment);

builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build(); 

app.UseMiddleware<ExceptionsMiddleware>();

app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Database Initialization and Seeding (CONDITIONAL using SeedDatabase class)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DataContext>(); // Get your DataContext
        var environment = services.GetRequiredService<IWebHostEnvironment>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        // Apply Migrations - Ensure database is created and schema is up-to-date
        context.Database.Migrate();

        // Seed Database using SeedDatabase.Initialize - conditional logic is inside SeedDatabase.Initialize
        logger.LogInformation("Initializing database seeding (SeedDatabase.Initialize)...");
        SeedDatabase.Initialize(services, app.Environment); // Pass service provider and environment
        logger.LogInformation("Database seeding completed via SeedDatabase.Initialize.");

    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during database initialization and seeding.");
    }
}

app.Run();
