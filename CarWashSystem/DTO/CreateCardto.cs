using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarWashSystem.DTO
{
    public class CreateCardto
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string FileName { get; set; }


        public string CarType { get; set; }

        public string CarNumber { get; set; }

        public int UserId { get;set; }


    }
}
