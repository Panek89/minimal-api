using minimal_api.Requests;

namespace minimal_api.Routing
{
    public static class CarsRouting
    {
        public static IEndpointRouteBuilder MapCarsApi(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/cars", CarRequests.GetAllCars);
            routes.MapGet("/cars/{id}", CarRequests.GetById);
            routes.MapPost("/cars", CarRequests.Create);
            routes.MapPut("/cars/{id}", CarRequests.Update);
            routes.MapDelete("/cars/{id}", CarRequests.Delete);
            
            return routes;
        }
    }
}
