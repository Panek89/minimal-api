using minimal_api.Entities;
using minimal_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICarService, CarService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/cars", (ICarService service) => service.GetAllCars());
app.MapGet("/cars/{id}", (ICarService service, Guid id) => service.GetById(id));
app.MapPost("/cars", (ICarService service, Car car) => service.Create(car));
app.MapPut("/cars/{id}", (ICarService service, Guid id, Car car) => service.Update(id, car));
app.MapDelete("/cars/{id}", (ICarService service, Guid id) => service.Delete(id));  

app.Run();
