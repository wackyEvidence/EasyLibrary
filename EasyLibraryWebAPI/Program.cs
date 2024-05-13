using EasyLibrary.Application.Services;
using EasyLibrary.DataAccess;
using EasyLibrary.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddDbContext<EasyLibraryDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(EasyLibraryDbContext)));
});

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapControllers();

app.Run();
