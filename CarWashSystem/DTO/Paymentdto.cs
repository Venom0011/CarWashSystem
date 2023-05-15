using CarWashSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarWashSystem.DTO
{
    public class Paymentdto
    {
        [Required]
        public int Id { get; set; }

        public string CardHolderName { get; set; }

       
        public string CardType { get; set; }

       
        public string CardNumber { get; set; }


        public string PaymentStatus { get; set; }

        public double TotalAmount { get; set; }


  

        public int UserId { set; get; }

    }
}
