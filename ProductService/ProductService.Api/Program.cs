using Microsoft.EntityFrameworkCore;
using ProductService.DAL;
using ProductService.DAL.ProductContext;
using ProductService.DAL.Repositories;
using ProductService.Services.Services;
using Shared.DatabaseInitializer;
using Shared.Extencions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionString"], b => b.MigrationsAssembly("ProductService.DAL")));

var signinKey = builder.Configuration.GetValue<string>("Key")!;

builder.Services.AddScoped<IDatabaseInitializer, ProductDataBaseInitializer>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IProductService, ProductService.Services.Services.ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();

builder.Services.ConfigureSwagger();

builder.Services.ConfigureAuthorization(signinKey);

builder.Services.ConfigureModelStateErrors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.InitDB().Run();