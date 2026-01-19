using System.ComponentModel.DataAnnotations;

namespace ECommerce_WebApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        // NEVER store plain passwords
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        // Navigation
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
