using Microsoft.EntityFrameworkCore;
using minimal_api.DB;
using minimal_api.Entities;

namespace minimal_api.Services
{
    public class CarService : ICarService
    {
        private readonly MinApiContext _context;
        public CarService(MinApiContext context)
        {
            _context = context;
        }

        private readonly Dictionary<Guid, Car> _cars = new();

        public async Task<List<Car>> GetAllCars()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetById(Guid id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task Create(Car car)
        {
            if (car is null)
            {
                return;
            }
            
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Guid id, Car car)
        {

            var existingCar = await _context.Cars.FindAsync(id);
            if(existingCar is null) 
            {
                return;
            }
            existingCar.Manufacturer = car.Manufacturer;
            existingCar.Model = car.Model;

            await _context.SaveChangesAsync();
        }
        
        public async Task Delete(Guid id)
        {
            var carToDelete = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(carToDelete);

            await _context.SaveChangesAsync();
        }
    }
}