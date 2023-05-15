using CarWashSystem.Models;

namespace CarWashSystem.Interfaces
{
    public interface ICar
    {
        Task<List<Car>> GetCars();
        Task<Car> GetCarById(int id);
        Task<Car> AddCar(Car car);
        Task<Car> DeleteCar(int id);

        Task<Car> UpdateCar(int id,Car car);

        Task<List<Car>> GetCarwithUser();
    }
}
