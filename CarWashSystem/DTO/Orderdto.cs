using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CarWashSystem.Models;

namespace CarWashSystem.DTO
{
    public class Orderdto
    {
        [Required]
        public int Id { get; set; }

        public DateTime scheduledatetime { get; set; }

        public string PickUpPoint { get; set; }
        public string WashingStatus { get; set; }
        

        public int UserId { get; set; }
        public int WashPackageId { get; set; }

        public int PaymentId { get; set; }
 
        public int CarId { get; set; }
    }
}
