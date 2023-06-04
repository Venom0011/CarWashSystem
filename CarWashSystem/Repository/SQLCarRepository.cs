using Azure.Core;
using CarWashSystem.Data;
using CarWashSystem.Interfaces;
using CarWashSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace CarWashSystem.Repository
{
    public class SQLCarRepository : ICar
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly OnDemandDbContext context;

        public SQLCarRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor, 
            OnDemandDbContext context) 
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
        }
        public async Task<Car> AddCar(Car car)
        {
            //setting the local path
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
               $"{car.FileName}{car.FileExtension}");

            //upload file to path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await car.File.CopyToAsync(stream);

            //converting the upload location to
            // https://localhost:7200/images/image.jpg format using IHttpContextAccessor


            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{car.FileName}{car.FileExtension}";
            car.FilePath = urlFilePath;
            
            var Data = context.Cars.Where(x => x.CarNumber == car.CarNumber);
            if (Data.Count() > 0)
            {
                throw new  BadHttpRequestException("Car already added");
            }
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
            //existingdata.CarImg = car.CarImg;
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
