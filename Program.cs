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

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var ctx = services.GetRequiredService<DataContext>();
    await ctx.Database.MigrateAsync();
    await Seed.SeedUsers( ctx );
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurr during migration");
    }

app.Run();
