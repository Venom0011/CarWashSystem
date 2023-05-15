using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CarWashSystem.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Schedule Date Required")]
        [DisplayName("Schedule Date")]
        public DateTime scheduledatetime { get; set; }

        [Required(ErrorMessage = "Pickup Point Required")]
        [DisplayName("Pickup Point")]
        public string PickUpPoint { get; set; }
        public string WashingStatus { get; set; }
        


        public User User { get; set; }
        public int UserId { get; set; }
       
        public WashPackage WashPackage { set; get; }
        public int WashPackageId { get; set; }

        
        public Payment Payment { set; get; }
        public int PaymentId { get; set; }
        public Car Car {  get; set; }
        public int CarId { get; set; }
     
    }
}
