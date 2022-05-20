using System.Text;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using minimal_api.Routing;
using minimal_api.Services;
using minimal_api.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICarService, CarService>();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CarValidator));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(cfg => 
    {
        cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
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

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var config = services.GetRequiredService<IConfiguration>();
    app.MapAuthApi(config);
}

app.Urls.Add("http://localhost:4000");
app.Urls.Add("https://localhost:4001");

app.Run();
