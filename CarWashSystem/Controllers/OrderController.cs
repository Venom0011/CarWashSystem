using CarWashSystem.DTO;
using CarWashSystem.Interfaces;
using CarWashSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarWashSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder orderrepo;

        public OrderController(IOrder orderrepo) 
        {
            this.orderrepo = orderrepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders= await orderrepo.GetOrders();
            var orderdto = new List<Orderdto>();
            foreach (var order in orders)
            {

                orderdto.Add(new Orderdto()
                {
                    Id = order.Id,
                    scheduledatetime = order.scheduledatetime,
                    PickUpPoint = order.PickUpPoint,
                    WashingStatus = order.WashingStatus,
                    UserId=order.UserId,
                    WashPackageId = order.WashPackageId,
                    PaymentId=order.PaymentId,
                    CarId=order.CarId
                });
            }

            return Ok(orderdto);
        }

        [HttpGet("GetOrderswithAllDetails")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderswithAllDetails()
        {
            return await orderrepo.GetOrderswithAllDetails();
        }

        //Get User by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order= await orderrepo.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderdto = new Orderdto()
            {
                Id = order.Id,
                scheduledatetime = order.scheduledatetime,
                PickUpPoint = order.PickUpPoint,
                WashingStatus = order.WashingStatus,
                UserId=order.UserId,
                WashPackageId = order.WashPackageId,
                PaymentId = order.PaymentId,
                CarId = order.CarId
            };

            return Ok(orderdto);
        }

        //Add User
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(CreateOrderdto createOrderdto)
        {

            var order = new Order()
            {
                scheduledatetime = createOrderdto.scheduledatetime,
                PickUpPoint = createOrderdto.PickUpPoint,
                WashingStatus = createOrderdto.WashingStatus,
                UserId = createOrderdto.UserId,
                WashPackageId = createOrderdto.WashPackageId,
                PaymentId = createOrderdto.PaymentId,
                CarId = createOrderdto.CarId
            };
            order = await orderrepo.AddOrder(order);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order= await orderrepo.DeleteOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
