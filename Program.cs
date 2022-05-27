using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using minimal_api.DB;
using minimal_api.DB.Seed;
using minimal_api.Entities;
using minimal_api.Routing;
using minimal_api.Services;
using minimal_api.Services.Auth;
using minimal_api.Services.UserService;
using minimal_api.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDbSeeder, DbSeeder>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CarValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(UserValidator));
builder.Services.AddDbContext<MinApiContext>(
    options => options.UseSqlite("Data Source=MinimalApi.sqlite;"));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(cfg => 
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JwtValidIssuer"],
            ValidAudience = builder.Configuration["JwtValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSigningKey"]))
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCarsApi();
app.MapUserApi();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var config = services.GetRequiredService<IConfiguration>();
    app.MapAuthApi(config);

    var seeder = services.GetRequiredService<IDbSeeder>();
    seeder.Seed();
}

app.Urls.Add("http://localhost:4000");
app.Urls.Add("https://localhost:4001");

app.Run();
