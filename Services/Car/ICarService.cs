using minimal_api.Entities;

namespace minimal_api.Services
{
    public interface ICarService
    {
        List<Car> GetAllCars();
        Car GetById(Guid id);
        void Create(Car car);
        void Update(Guid id, Car car);
        void Delete(Guid id);
    }
}