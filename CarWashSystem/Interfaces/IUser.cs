using CarWashSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarWashSystem.Interfaces
{
    public interface IUser
    {
        Task<List<User>> GetUsers(string? fiteredQuery=null, string? sortBy=null, bool isAscending=true,int pageNumber=1,int pageSize = 1000);
        Task<User> GetUserById(int id);
        Task<User> CreateUser(User user);
       
        Task<User> DeleteUser(int id);
    }
}
