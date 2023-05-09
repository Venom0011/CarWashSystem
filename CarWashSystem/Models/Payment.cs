using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CarWashSystem.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

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
        public double TotalAmount { get; set; }

        [JsonIgnore]
        public User User { set; get; }  
        
       
    }
}
