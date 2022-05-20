using minimal_api.DB;
using minimal_api.Entities;
using minimal_api.Services;

namespace minimal_api.Requests
{
    public class CarRequests
    {
        public static IResult GetAllCars(ICarService service)
        {
            var cars = service.GetAllCars();

            return Results.Ok(cars);
        }

        public static IResult GetById(ICarService service, Guid id)
        {
            var carById = service.GetById(id);
            if (carById is null)
                return Results.NotFound();

            return Results.Ok(carById);
        }

        public static IResult Create(ICarService service, Car car)
        {       
            service.Create(car);

            return Results.Created($"/cars/{car.Id}", car);
        }

        public static IResult Update(ICarService service, Guid id, Car car)
        {
            using (var context = new CarsContext())
            {
                var carForUpdate = context.Cars.Find(id);
                if (carForUpdate is null)
                    return Results.NotFound();
            }
            service.Update(id, car);

            return Results.NoContent();
        }

        public static IResult Delete(ICarService service, Guid id)
        {
            using (var context = new CarsContext())
            {
                var carForDelete = context.Cars.Find(id);
                if (carForDelete is null)
                    return Results.NotFound();
            }
            service.Delete(id);

            return Results.NoContent();
        }
    }

}
