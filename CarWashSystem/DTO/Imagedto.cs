using CarWashSystem.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarWashSystem.DTO
{
    public class Imagedto
    {
        public int ImageId { get; set; }

        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string FileName { get; set; }


        [Key]
        public int CarId { get; set; }

        public string CarType { get; set; }
     
        public string CarNumber { get; set; }

        public int UserId { get; set; }

    }
}
