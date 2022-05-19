using minimal_api.Entities;
using minimal_api.Extensions;
using minimal_api.Requests;

namespace minimal_api.Routing
{
    public static class CarsRouting
    {
        public static IEndpointRouteBuilder MapCarsApi(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/cars", CarRequests.GetAllCars)
                .Produces<List<Car>>(StatusCodes.Status200OK)
                .RequireAuthorization();

            routes.MapGet("/cars/{id}", CarRequests.GetById)
                .Produces<Car>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .RequireAuthorization();

            routes.MapPost("/cars", CarRequests.Create)
                .Accepts<Car>("Application/Json")
                .Produces<Car>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .WithValidator<Car>();

            routes.MapPut("/cars/{id}", CarRequests.Update)
                .Accepts<Car>("Application/Json")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .WithValidator<Car>();

            routes.MapDelete("/cars/{id}", CarRequests.Delete)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound);

            
            return routes;
        }
    }
}
