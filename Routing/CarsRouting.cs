using minimal_api.Entities;
using minimal_api.Services;

namespace minimal_api.Routing
{
    public static class CarsRouting
    {
        public static IEndpointRouteBuilder MapCarsApi(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/cars", (ICarService service) => service.GetAllCars());
            routes.MapGet("/cars/{id}", (ICarService service, Guid id) => service.GetById(id));
            routes.MapPost("/cars", (ICarService service, Car car) => service.Create(car));
            routes.MapPut("/cars/{id}", (ICarService service, Guid id, Car car) => service.Update(id, car));
            routes.MapDelete("/cars/{id}", (ICarService service, Guid id) => service.Delete(id));  
            return routes;
        }
    }
}
