using FluentValidation;
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

        public static IResult Create(ICarService service, Car car, IValidator<Car> validator)
        {
            var validationResult = validator.Validate(car);
            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors);
                
            service.Create(car);

            return Results.Created($"/cars/{car.Id}", car);
        }

        public static IResult Update(ICarService service, Guid id, Car car)
        {
            var carForUpdate = service.GetById(id);
            if (carForUpdate is null)
                return Results.NotFound();

            service.Update(id, car);
            return Results.NoContent();
        }

        public static IResult Delete(ICarService service, Guid id)
        {
            var carForDelete = service.GetById(id);
            if (carForDelete is null)
                return Results.NotFound();

            service.Delete(id);
            return Results.NoContent();
        }
    }

}
