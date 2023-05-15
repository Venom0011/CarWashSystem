using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarWashSystem.DTO
{
    public class CreateCardto
    {

        public string CarType { get; set; }

        public string CarNumber { get; set; }
      
        public string? CarImg { get; set; }
        
        public int UserId { get;set; }
    }
}
