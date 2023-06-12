using Microsoft.EntityFrameworkCore;
using Shared.DatabaseInitializer;
using Shared.Extencions;
using UserService.DAL;
using UserService.DAL.Repositories;
using UserService.DAL.UserContext;
using UserService.Services;
using UserService.Services.Services;
using UserService.Services.Services.Abstract;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionString"], b => b.MigrationsAssembly("UserService.DAL")));

builder.Services.AddScoped<IDatabaseInitializer, UserDataBaseInitializer>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPaswordHasherService, PaswordHasherService>();
builder.Services.AddScoped<IUserService, UserService.Services.Services.UserService>();

var signinKey = builder.Configuration.GetValue<string>("Key")!;

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