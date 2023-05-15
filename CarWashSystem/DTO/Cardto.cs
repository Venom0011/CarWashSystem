using System.ComponentModel.DataAnnotations;

namespace CarWashSystem.DTO
{
    public class Cardto
    {
        [Required]
        public int Id { get; set; }
        public string CarType { get; set; }

        public string CarNumber { get; set; }

        public string? CarImg { get; set; }

        public int UserId { get; set; }
    }
}
