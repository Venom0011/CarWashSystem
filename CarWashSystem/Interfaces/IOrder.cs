using CarWashSystem.Models;

namespace CarWashSystem.Interfaces
{
    public interface IOrder
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(int id);
        Task<Order> AddOrder(Order order);
        Task<Order> DeleteOrder(int id);

        Task<List<Order>> GetOrderswithAllDetails();
    }
}
