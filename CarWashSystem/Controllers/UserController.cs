using CarWashSystem.Data;
using CarWashSystem.DTO;
using CarWashSystem.Models;
using CarWashSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarWashSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly OnDemandDbContext _context;
        private readonly IUser userrepository;

        public UserController(OnDemandDbContext context,IUser userrepository)
        {
            _context = context;
            this.userrepository = userrepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var users= await userrepository.GetUsers();
            if (_context.Users == null)
            {
                return NotFound();
            }
            var userdto = new List<Userdto>();
            foreach (var user in users)
            {
                
                userdto.Add(new Userdto()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Password = user.Password,
                    Address= user.Address,
                    Role=user.Role
                });
            }

            return Ok(userdto); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserById(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user= await userrepository.GetUserById(id);
            if(user == null)
            {
                return NotFound();
            }
            var userdto = new Userdto()
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Password = user.Password,
                Address = user.Address,
                Role = user.Role
            };

            return Ok(userdto);
        }

        
        [HttpPost]
        public async Task<ActionResult<IEnumerable<User>>> PostUser(CreateUserdto createuser)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = new User() {
                FullName = createuser.FullName,
                Email = createuser.Email,
                Password = createuser.Password,
                Address = createuser.Address,
                Role = createuser.Role
        };
            user = await userrepository.CreateUser(user);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id,UserUpdatedto userUpdatedto)
        {
            var user = new User()
            {
                FullName = userUpdatedto.FullName,
                Email = userUpdatedto.Email,
                Password = userUpdatedto.Password,
                Address = userUpdatedto.Address,
            };
            if(_context.Users == null)
            {
                return NotFound();
            }
            user = await userrepository.UpdateUser(id,user);
            
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.FullName = userUpdatedto.FullName;
                user.Email = userUpdatedto.Email;
                user.Password = userUpdatedto.Password;
                user.Address = userUpdatedto.Address;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteUser(int id)
        {
            if(_context.Users == null)
            {
                return NotFound();
            }
            var user = await userrepository.DeleteUser(id);
            if (user == null)
            {
                return NotFound();
            }
            // no asyn method for remove so no await for remove

            return Ok(user);
        }
    }
}
