using EasyLibrary.Application.Services;
using EasyLibrary.Core.Abstractions;
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess;
using EasyLibrary.DataAccess.Entites;
using EasyLibrary.DataAccess.Mappers;
using EasyLibrary.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddDbContext<EasyLibraryDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(EasyLibraryDbContext)));
});


#region Mappers 
builder.Services.AddTransient(provider =>
{
    return new Lazy<IMapper<BookAuthorEntity, BookAuthor>>(() => provider.GetRequiredService<IMapper<BookAuthorEntity, BookAuthor>>());
});

builder.Services.AddTransient(provider =>
{
    return new Lazy<IMapper<BookCopyEntity, BookCopy>>(() => provider.GetRequiredService<IMapper<BookCopyEntity, BookCopy>>());
});

builder.Services.AddTransient(provider =>
{
    return new Lazy<IMapper<BookSeriesEntity, BookSeries>>(() => provider.GetRequiredService<IMapper<BookSeriesEntity, BookSeries>>());
});

builder.Services.AddTransient(provider =>
{
    return new Lazy<IMapper<BookTypeEntity, BookType>>(() => provider.GetRequiredService<IMapper<BookTypeEntity, BookType>>());
});

builder.Services.AddTransient(provider =>
{
    return new Lazy<IMapper<PublishingHouseEntity, PublishingHouse>>(() => provider.GetRequiredService<IMapper<PublishingHouseEntity, PublishingHouse>>());
});

builder.Services.AddTransient<IMapper<BookAuthorEntity, BookAuthor>, BookAuthorMapper>();
builder.Services.AddTransient<IMapper<BookCopyEntity, BookCopy>, BookCopyMapper>();
builder.Services.AddTransient<IMapper<BookSeriesEntity, BookSeries>, BookSeriesMapper>();
builder.Services.AddTransient<IMapper<BookTypeEntity, BookType>, BookTypeMapper>();
builder.Services.AddTransient<IMapper<PublishingHouseEntity, PublishingHouse>, PublishingHouseMapper>();
#endregion

#region Services 
builder.Services.AddScoped<IBookAuthorsService, BookAuthorsService>();
builder.Services.AddScoped<IBookSeriesService, BookSeriesService>();
builder.Services.AddScoped<IPublishingHouseService, PublishingHousesService>();
builder.Services.AddScoped<IUsersService, UsersService>();
#endregion

#region Repositories 
builder.Services.AddScoped<IBookAuthorsRepository, BookAuthorsRepository>();
builder.Services.AddScoped<IBookSeriesRepository, BookSeriesRepository>();
builder.Services.AddScoped<IPublishingHouseRepository, PublishingHouseRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseStaticFiles();
app.UseAuthorization();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapControllers();

app.Run();
