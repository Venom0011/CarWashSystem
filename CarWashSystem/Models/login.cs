using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CarWashSystem.Models
{
    public class login
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        
        public string Address { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}
