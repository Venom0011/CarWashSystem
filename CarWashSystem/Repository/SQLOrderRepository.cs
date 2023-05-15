using CarWashSystem.Data;
using CarWashSystem.Interfaces;
using CarWashSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarWashSystem.Repository
{
    public class SQLOrderRepository : IOrder
    {
        private readonly OnDemandDbContext context;

        public SQLOrderRepository(OnDemandDbContext context) 
        {
            this.context = context;
        }
        public async Task<Order> AddOrder(Order order)
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> DeleteOrder(int id)
        {
            var order = await context.Orders.FindAsync(id);
            context.Orders.Remove(order);
            await context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            return order;
        }

        public async Task<List<Order>> GetOrders()
        {
            return await context.Orders.ToListAsync();
        }

        public async Task<List<Order>> GetOrderswithAllDetails()
        {
            return await context.Orders.Include(x=>x.User).Include(x=>x.WashPackage).Include(x=>x.Payment).Include(x=>x.Car).ToListAsync();
        }
    }
}
