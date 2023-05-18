using CarWashSystem.Data;
using CarWashSystem.DTO;
using CarWashSystem.Interfaces;
using CarWashSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarWashSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       
        private readonly IUser userrepository;

        public UserController(IUser userrepository)
        {
            this.userrepository = userrepository;
        }

        //Get all user
        [HttpGet,Authorize(Roles ="Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var users= await userrepository.GetUsers();
           
            var userdto = new List<Userdto>();
            foreach (var user in users)
            {
                
                userdto.Add(new Userdto()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Address= user.Address,
                    Role=user.Role
                });
            }

            return Ok(userdto); 
        }

        //Get User by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserById(int id)
        {
            

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
                Address = user.Address,
                Role = user.Role
            };

            return Ok(userdto);
        }

        //Add User
        [HttpPost,Authorize(Roles ="Customer")]
        public async Task<ActionResult<IEnumerable<User>>> PostUser(CreateUserdto createuser)
        {
            
            var user = new User() {
                FullName = createuser.FullName,
                Email = createuser.Email,
                Address = createuser.Address,
                Role = createuser.Role
        };
            user = await userrepository.CreateUser(user);
            return Ok();
        }

        //Update User
        

        //Delete User
        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteUser(int id)
        {
           
            var user = await userrepository.DeleteUser(id);
            if (user == null)
            {
                return NotFound();
            }
            // no asyn method for remove so no await for remove

            return Ok();
        }
    }
}
