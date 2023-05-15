using CarWashSystem.DTO;
using CarWashSystem.Interfaces;
using CarWashSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarWashSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICar carrepo;

        public CarController(ICar carrepo)
        {
            this.carrepo = carrepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            var cars=await carrepo.GetCars();
            var cardto = new List<Cardto>();
            foreach (var car in cars)
            {

                cardto.Add(new Cardto()
                {
                    Id=car.Id,
                    CarType = car.CarType,
                    CarNumber = car.CarNumber,
                    CarImg = car.CarImg,
                    UserId = car.UserId
                });
            }
            return Ok(cardto);
        }

        [HttpGet("GetCarsWithUser")]
        public async Task<ActionResult<IEnumerable<Car>>> GetCarsWithUser()
        {
            return await carrepo.GetCarwithUser();
        }

        //Get User by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCarById(int id)
        {
            var car= await carrepo.GetCarById(id);
            if (car == null)
            {
                return BadRequest();
            }
            var cardto = new Cardto()
            {
                Id = car.Id,
                CarType = car.CarType,
                CarNumber = car.CarNumber,
                CarImg = car.CarImg,
                UserId = car.UserId
            };
            return Ok(cardto);
        }

        //Add User
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(CreateCardto createCardto)
        {

            var car = new Car()
            {
                CarType = createCardto.CarType,
                CarNumber = createCardto.CarNumber,
                CarImg = createCardto.CarImg,
                UserId=createCardto.UserId
                
            };
            car = await carrepo.AddCar(car);
            return Ok(car);
        }

        //Update Car
        [HttpPut("{id}")]
        public async Task<ActionResult<Car>> UpdateCar(int id,CreateCardto createCardto) 
        {
            var car = new Car()
            {
                CarType = createCardto.CarType,
                CarNumber = createCardto.CarNumber,
                CarImg = createCardto.CarImg,
                UserId = createCardto.UserId
            };

            car = await carrepo.UpdateCar(id, car);

            if (car == null)
            {
                return NotFound();
            }
            else
            {
                car.CarType = createCardto.CarType;
                car.CarNumber = createCardto.CarNumber;
                car.CarImg = createCardto.CarImg;
            }


            return Ok(car);
        }

        //Delete Car
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await carrepo.DeleteCar(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }
    }
}
