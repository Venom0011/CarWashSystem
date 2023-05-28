﻿using CarWashSystem.Data;
using CarWashSystem.DTO;
using CarWashSystem.Interfaces;
using CarWashSystem.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CarWashSystem.Controllers
{
    public class Response
    {
        public string token { get; set; }
        public User payload { get; set; }
        public Response() { }
    }

    public class Token
    {
        public string token { get; set; } = string.Empty;
    }


    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class AuthController : ControllerBase
    {
        private readonly OnDemandDbContext context;
        private readonly IConfiguration configuration;

        public AuthController(OnDemandDbContext context,IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

       

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(registerdto request)
        {
            var Data=context.Users.Where(x=>x.Email==request.Email);
            if(Data.Count() > 0 )
            {
                return BadRequest("Email already taken");
            }
            User user = new User();
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.FullName = request.FullName;
            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt= passwordSalt;
            user.Address = request.Address;
            user.Role = request.Role;

            
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(logindto request)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (user==null)
            {
                return BadRequest("User not found");
            }
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is not correct");
            }
            string token = CreateToken(user);
            return Ok(new Response { token = token, payload = user,  });
        }

        private string CreateToken(User login)
        {
            string check = "";
            if (login.Role == "Customer")
            {
                check = "Customer";
            }
            else if (login.Role == "Washer")
            {
                check = "Washer";
            }
            else
            {
                check = "Admin";
            }
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,login.Email),
                new Claim(ClaimTypes.Role,check)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials:cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private void CreatePasswordHash(string password,out byte[] passwordHash, out byte[] passwordSalt) 
        {
            using(var hmac=new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash,  byte[] passwordSalt)
        {
            using(var hmac=new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
