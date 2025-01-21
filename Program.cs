using Api.Extensions; 

var builder = WebApplication.CreateBuilder(args);

// add services to the container
builder.Services.AddAplicationServices(builder.Configuration, builder.Environment);

builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build(); 

app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
