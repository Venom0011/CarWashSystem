using CarWashSystem.DTO;
using CarWashSystem.Interfaces;
using CarWashSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarWashSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayment paymentrepo;
    
        public PaymentController(IPayment paymentrepo) 
        {
            this.paymentrepo = paymentrepo;
        }

        //Make Payment
        [HttpPost]
        public async Task<ActionResult<Payment>> Create(CreatePaymentdto createPaymentdto)
        {
            var payment = new Payment()
            {
                CardHolderName = createPaymentdto.CardHolderName,
                CardType = createPaymentdto.CardType,
                CardNumber = createPaymentdto.CardNumber,
                PaymentStatus = createPaymentdto.PaymentStatus,
                UserId = createPaymentdto.UserId,
                TotalAmount = createPaymentdto.TotalAmount
            };
            await paymentrepo.CreatePayment(payment);
            return Ok(payment);
        }

        // Get All Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetAll()
        {
            return await paymentrepo.GetPayment();
        }

        // Get payment by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetById(int id)
        {
            return await paymentrepo.GetPaymentById(id);
        }

        //Cancel Payment
        [HttpDelete("{id}")]

        public async Task<ActionResult<Payment>> Delete(int id)
        {
            var payment=await paymentrepo.DeletePayment(id);
            return Ok(payment);
        }

    }
}
