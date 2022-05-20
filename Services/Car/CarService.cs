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

        public List<Car> GetAllCars()
        {
            using (var context = new CarsContext())
            {
                return context.Cars.ToList();
            }
        }

        public Car GetById(Guid id)
        {
            using (var context = new CarsContext())
            {
                return context.Cars.Find(id);
            }
        }

        public void Create(Car car)
        {
            if (car is null)
            {
                return;
            }
            
            using (var context = new CarsContext())
            {
                context.Cars.Add(car);
                context.SaveChanges();
            }
        }

        public void Update(Guid id, Car car)
        {
            using (var context = new CarsContext())
            {
                var existingCar = context.Cars.Find(id);
                if(existingCar is null) 
                {
                    return;
                }
                existingCar.Manufacturer = car.Manufacturer;
                existingCar.Model = car.Model;

                context.SaveChanges();
            }
        }
        
        public void Delete(Guid id)
        {
            using (var context = new CarsContext())
            {
                var carToDelete = context.Cars.Find(id);
                context.Cars.Remove(carToDelete);

                context.SaveChanges();
            }
        }
    }
}