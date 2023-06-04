using Azure.Core;
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
                    
                    FileName = car.FileName,
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
                FileName = car.FileName,
                UserId = car.UserId
            };
            return Ok(cardto);
        }

        //Add User
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar([FromForm] CreateCardto createCardto)
        {
            ValidateFileUpload(createCardto);
            var car = new Car()
            {
                CarType = createCardto.CarType,
                CarNumber = createCardto.CarNumber,
                File = createCardto.File,
                FileExtension = Path.GetExtension(createCardto.File.FileName),
                FileSizeInBytes=createCardto.File.Length,
                FileName = createCardto.FileName,
                UserId =createCardto.UserId
                
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
                //CarImg = createCardto.CarImg,
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
                //car.CarImg = createCardto.CarImg;
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
            return Ok();
        }

        private void ValidateFileUpload(CreateCardto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }
        }
    }
}
