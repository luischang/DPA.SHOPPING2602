using DPA.SHOPPING.CORE.Core.Interfaces;
using DPA.SHOPPING.CORE.Core.Services;
using DPA.SHOPPING.CORE.Infrastructure.Data;
using DPA.SHOPPING.CORE.Infrastructure.Repositories;
using DPA.SHOPPING.CORE.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using OllamaSharp;

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
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddSharedInfrastructure(_config);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//IA IChatClient provider selected
IChatClient chatClient = _config["AI:Provider"] == "Gemini" ? 
        new Google.GenAI.Client(apiKey: _config["AI:Gemini:ApiKey"]).AsIChatClient(_config["AI:Gemini:Model"]) :
        new OllamaApiClient(new Uri(_config["AI:Ollama:Endpoint"]!), _config["AI:Ollama:Model"]!);
builder.Services.AddChatClient(chatClient);

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
