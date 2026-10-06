using DPA.SHOPPING.CORE.Core.Interfaces;
using DPA.SHOPPING.CORE.Core.Services;
using DPA.SHOPPING.CORE.Infrastructure.Data;
using DPA.SHOPPING.CORE.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var _config = builder.Configuration;
var cnx = _config.GetConnectionString("DevConnection");
builder
    .Services
    .AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(cnx)
    );

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddTransient<IFavoriteService, FavoriteService>();
builder.Services.AddTransient<IOrdersRepository, OrdersRepository>();
builder.Services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddTransient<IOrdersService, OrdersService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Shopping API v1");        
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
