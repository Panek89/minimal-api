using minimal_api.DB;
using minimal_api.Entities;
using minimal_api.Services;

namespace minimal_api.Requests
{
    public class CarRequests
    {
        public static async Task<IResult> GetAllCars(ICarService service)
        {
            var cars = await service.GetAllCars();

            return Results.Ok(cars);
        }

        public static async Task<IResult> GetById(ICarService service, Guid id)
        {
            var carById = await service.GetById(id);
            if (carById is null)
                return Results.NotFound();

            return Results.Ok(carById);
        }

        public static async Task<IResult> Create(ICarService service, Car car)
        {       
            await service.Create(car);

            return Results.Created($"/cars/{car.Id}", car);
        }

        public static async Task<IResult> Update(ICarService service, Guid id, Car car)
        {
            using (var context = new CarsContext())
            {
                var carForUpdate = await context.Cars.FindAsync(id);
                if (carForUpdate is null)
                    return Results.NotFound();
            }
            await service.Update(id, car);

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
