using Microsoft.EntityFrameworkCore;
using minimal_api.DB;
using minimal_api.Entities;

namespace minimal_api.Services
{
    public class CarService : ICarService
    {
        public CarService()
        {
            var sampleCar = new Car { Manufacturer = "BMW", Model = "E36" };
            _cars[sampleCar.Id] = sampleCar;
        }

        private readonly Dictionary<Guid, Car> _cars = new();

        public async Task<List<Car>> GetAllCars()
        {
            using (var context = new CarsContext())
            {
                return await context.Cars.ToListAsync();
            }
        }

        public async Task<Car> GetById(Guid id)
        {
            using (var context = new CarsContext())
            {
                return await context.Cars.FindAsync(id);
            }
        }

        public async Task Create(Car car)
        {
            if (car is null)
            {
                return;
            }
            
            using (var context = new CarsContext())
            {
                await context.Cars.AddAsync(car);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(Guid id, Car car)
        {
            using (var context = new CarsContext())
            {
                var existingCar = await context.Cars.FindAsync(id);
                if(existingCar is null) 
                {
                    return;
                }
                existingCar.Manufacturer = car.Manufacturer;
                existingCar.Model = car.Model;

                await context.SaveChangesAsync();
            }
        }
        
        public async Task Delete(Guid id)
        {
            using (var context = new CarsContext())
            {
                var carToDelete = await context.Cars.FindAsync(id);
                context.Cars.Remove(carToDelete);

                await context.SaveChangesAsync();
            }
        }
    }
}