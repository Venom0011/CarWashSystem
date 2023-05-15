using CarWashSystem.Data;
using CarWashSystem.Interfaces;
using CarWashSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarWashSystem.Repository
{
    public class SQLCarRepository : ICar
    {
        private readonly OnDemandDbContext context;

        public SQLCarRepository(OnDemandDbContext context) 
        {
            this.context = context;
        }
        public async Task<Car> AddCar(Car car)
        {
            await context.Cars.AddAsync(car);
            await context.SaveChangesAsync();
            return car;
        }

        public async Task<Car> DeleteCar(int id)
        {
            var car = await context.Cars.FirstOrDefaultAsync(x => x.Id == id);
            context.Cars.Remove(car);
            await context.SaveChangesAsync();

            return car;
        }

        public async Task<Car> GetCarById(int id)
        {
            var car = await context.Cars.FirstOrDefaultAsync(x => x.Id == id);
            return car;
        }

        public async Task<List<Car>> GetCars()
        {
            return await context.Cars.ToListAsync();
        }

        public async Task<Car> UpdateCar(int id, Car car)
        {
            var existingdata = await context.Cars.FirstOrDefaultAsync(x => x.Id == id);
            if (car == null)
            {
                return null;
            }

            existingdata.CarNumber = car.CarNumber;
            existingdata.CarType = car.CarType;
            existingdata.CarImg = car.CarImg;
            existingdata.UserId = car.UserId;
            await context.SaveChangesAsync();
            return existingdata;
        }
        public async Task<List<Car>> GetCarwithUser()
        {
            var car = await context.Cars.Include(x => x.User).ToListAsync();
            return car;
        }
    }
}
