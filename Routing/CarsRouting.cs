using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using minimal_api.Dtos;
using minimal_api.Entities;
using minimal_api.Extensions;
using minimal_api.Models.Consts;
using minimal_api.Requests;
using minimal_api.Services;

namespace minimal_api.Routing
{
    public static class CarsRouting
    {
        public static IEndpointRouteBuilder MapCarsApi(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/cars", [Authorize(ConfiguredUserPoliciesValues.AdminsAndUsers)] ([FromServices] ICarService service) => CarRequests.GetAllCars(service))
                .Produces<List<Car>>(StatusCodes.Status200OK)
                .WithTags(ApiNamesValues.CarsApi);

            routes.MapGet("/cars/{id}", ([FromServices] ICarService service, [FromQuery] Guid id) => CarRequests.GetById(service, id))
                .Produces<Car>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags(ApiNamesValues.CarsApi);

            routes.MapPost("/cars", ([FromServices] ICarService service, [FromBody] Car car) => CarRequests.Create(service, car))
                .Accepts<CarDto>("Application/Json")
                .Produces<Car>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .WithValidator<Car>()
                .WithTags(ApiNamesValues.CarsApi);

            routes.MapPut("/cars/{id}", ([FromServices] ICarService service, [FromQuery] Guid id, [FromBody] Car car) => CarRequests.Update(service, id, car))
                .Accepts<CarDto>("Application/Json")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .WithValidator<Car>()
                .WithTags(ApiNamesValues.CarsApi);

            routes.MapDelete("/cars/{id}", ([FromServices] ICarService service, [FromQuery] Guid id) => CarRequests.Delete(service, id))
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags(ApiNamesValues.CarsApi);
            
            return routes;
        }
    }
}
