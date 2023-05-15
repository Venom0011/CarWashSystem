using CarWashSystem.Data;
using CarWashSystem.Interfaces;
using CarWashSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarWashSystem.Repository
{
    public class SQLPaymentRepository : IPayment
    {
        private readonly OnDemandDbContext context;

        public SQLPaymentRepository(OnDemandDbContext context) 
        {
            this.context = context;
        }

        public async Task<Payment> CreatePayment(Payment payment)
        {
            await context.Payments.AddAsync(payment);
            await context.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment> DeletePayment(int id)
        {
            var payment = await context.Payments.FindAsync(id);
            context.Payments.Remove(payment);
            await context.SaveChangesAsync();

            return payment;
        }

        public async Task<List<Payment>> GetPayment()
        {
            return await context.Payments.ToListAsync();
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            var payment = await context.Payments.FirstOrDefaultAsync(x=>x.Id==id);
            return payment;
        }

        public async Task<List<Payment>> GetPaymentwithUser()
        {
            var payment = await context.Payments.Include(x => x.User).ToListAsync();
            return payment;
        }
    }
}
