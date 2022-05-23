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
            var carForUpdate = await service.GetById(id);
            if (carForUpdate is null)
                return Results.NotFound();

            await service.Update(id, car);

            return Results.NoContent();
        }

        public static async Task <IResult> Delete(ICarService service, Guid id)
        {
            var carForDelete = await service.GetById(id);
            if (carForDelete is null)
                return Results.NotFound();

            await service.Delete(id);

            return Results.NoContent();
        }
    }

}
