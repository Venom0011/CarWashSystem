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
            return Ok();
        }

        // Get All Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetAll()
        {
            var payments = await paymentrepo.GetPayment();
            var paymentdto = new List<Paymentdto>();
            foreach (var payment in payments)
            {

                paymentdto.Add(new Paymentdto()
                {
                    Id = payment.Id,
                    CardHolderName = payment.CardHolderName,
                    CardType = payment.CardType,
                    CardNumber = payment.CardNumber,
                    PaymentStatus = payment.PaymentStatus,
                    TotalAmount=payment.TotalAmount,
                    UserId=payment.UserId
                });
            }

            return Ok(paymentdto);
        }

        [HttpGet("GetPayemntwithUser")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayemntwithUser()
        {
            return await paymentrepo.GetPaymentwithUser();
        }

        // Get payment by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetById(int id)
        {
            var payment= await paymentrepo.GetPaymentById(id);
            if(payment==null)
            {
                return NotFound();
            }
            var paymentdto = new Paymentdto()
            {
                Id = payment.Id,
                CardHolderName = payment.CardHolderName,
                CardType=payment.CardType, 
                CardNumber=payment.CardNumber,
                PaymentStatus = payment.PaymentStatus,
                TotalAmount=payment.TotalAmount,
                UserId = payment.UserId
            };
            return Ok(payment);
        }

        //Cancel Payment
        [HttpDelete("{id}")]

        public async Task<ActionResult<Payment>> Delete(int id)
        {
            var payment=await paymentrepo.DeletePayment(id);
            return Ok();
        }

    }
}
