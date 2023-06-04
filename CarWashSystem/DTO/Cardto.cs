using System.ComponentModel.DataAnnotations;

namespace CarWashSystem.DTO
{
    public class Cardto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName{ get; set;}
        
        public long FileSizeInBytes { get; set; }
        
        public string CarType { get; set; }

        public string CarNumber { get; set; }

        public int UserId { get; set; }
    }
}
