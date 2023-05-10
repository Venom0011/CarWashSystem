using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarWashSystem.DTO
{
    public class CreatePaymentdto
    {
        [Required(ErrorMessage = "Card Holder name is required")]
        [DisplayName("Card Holder Name")]
        public string CardHolderName { get; set; }

        [Required(ErrorMessage = "Card type is required")]
        [DisplayName("Card Type")]
        public string CardType { get; set; }

        [Required(ErrorMessage = "Card Number is required")]
        [DisplayName("Card Number")]
        public string CardNumber { get; set; }

        public string PaymentStatus { get; set; }

        public int UserId { get; set; }
        public double TotalAmount { get; set; }

        
    }
}
