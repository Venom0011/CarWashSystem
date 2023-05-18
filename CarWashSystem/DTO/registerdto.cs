using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarWashSystem.DTO
{
    public class registerdto
    {
        [Required(ErrorMessage = "Full Name is required")]
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        
        public string Address { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}
