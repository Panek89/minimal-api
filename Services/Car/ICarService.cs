using minimal_api.Entities;

namespace minimal_api.Services
{
    public interface ICarService
    {
        Dictionary<Guid, Car> GetAllCars();
        Car GetById(Guid id);
        void Create(Car car);
        void Update(Guid id, Car car);
        void Delete(Guid id);
    }
}