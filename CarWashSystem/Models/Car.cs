using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarWashSystem.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Car Type is required")]
        [DisplayName("Car Type")]
        public string CarType { get; set; }

        [Required(ErrorMessage = "Car Model is required")]
        [DisplayName("Car Model")]
        public string CarNumber { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        
        [JsonIgnore]
        public IEnumerable<Order> order { get; set; }




     
    }
}
