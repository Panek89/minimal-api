using minimal_api.Entities;

namespace minimal_api.Services
{
    public interface ICarService
    {
        Task<List<Car>> GetAllCars();
        Task<Car> GetById(Guid id);
        Task Create(Car car);
        Task Update(Guid id, Car car);
        Task Delete(Guid id);
    }
}