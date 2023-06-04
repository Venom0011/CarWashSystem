using CarWashSystem.Data;
using CarWashSystem.Interfaces;
using CarWashSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarWashSystem.Repository
{
    public class SQLUserRepository : IUser
    {
        private readonly OnDemandDbContext context;

        public SQLUserRepository(OnDemandDbContext context)
        {
            this.context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }



        public async Task<List<User>> GetUsers(string? fiteredQuery, string? sortBy = null, 
            bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var user = context.Users.AsQueryable();
            //filtering

            if(string.IsNullOrWhiteSpace(fiteredQuery)==false)
            {
                user = user.Where(x => x.Role.Contains(fiteredQuery));
            }

            //sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false) 
            {
                user = isAscending ? user.OrderBy(x => x.FullName) : user.OrderByDescending(x=>x.FullName);
            }

            //pagination
            var skipResult = (pageNumber - 1) * pageSize;

            return  await user.Skip(skipResult).Take(pageSize).ToListAsync();
            //return await context.Users.ToListAsync();
        }
        public async Task<User> GetUserById(int id)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Id == id);

        }

      
        public async Task<User> DeleteUser(int id)
        {
            var user=await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return user;
        }
    }
}
