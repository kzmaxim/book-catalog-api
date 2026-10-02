using BookCatalogApi.Data;
using BookCatalogApi.Repositories;
using BookCatalogApi.Repositories.impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddAutoMapper(cfg => 
{
    cfg.AddProfile<BookCatalogApi.Mappings.BookProfile>();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options => 
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Catalog API");
    options.RoutePrefix = "swagger"; 
});

app.MapControllers();
app.Run();