using CarWashSystem.Models;

namespace CarWashSystem.Interfaces
{
    public interface IPayment
    {
        Task<List<Payment>> GetPayment();
        Task<Payment> GetPaymentById(int id);
        Task<Payment> CreatePayment(Payment payment);
        Task<Payment> DeletePayment(int id);

        Task<List<Payment>> GetPaymentwithUser();
    }
}
