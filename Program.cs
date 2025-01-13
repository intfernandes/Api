using Api.Data; 
using Microsoft.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



if(builder.Environment.IsDevelopment() ){
// set db enviroment to development (sqlite on ./constance.db)
builder.Services.AddDbContext<DataContext >(opt => {
opt.UseSqlite( builder.Configuration.GetConnectionString("DefaultConnection"));
});

} else {
// set db enviroment to production
builder.Services.AddDbContext<DataContext >(opt => {
opt.UseSqlite( builder.Configuration.GetConnectionString("DefaultConnection"));
});
}
 

var app = builder.Build(); 

app.MapControllers();

app.Run();
