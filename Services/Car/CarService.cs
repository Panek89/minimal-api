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

        public Dictionary<Guid, Car> GetAllCars()
        {
            return _cars;
        }

        public Car GetById(Guid id)
        {
            return _cars.GetValueOrDefault(id);
        }

        public void Create(Car car)
        {
            if (car is null)
            {
                return;
            }

            _cars[car.Id] = car;
        }

        public void Update(Guid id, Car car)
        {
            var existingCar = GetById(id);

            if(existingCar is null) 
            {
                return;
            }
            
            _cars[id] = car;
        }
        public void Delete(Guid id)
        {
            _cars.Remove(id);
        }
    }
}