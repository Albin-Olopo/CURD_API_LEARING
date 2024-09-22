using System.ComponentModel.DataAnnotations;

namespace CURD_API_LEARING.Models
{
    public class User
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public required string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public required string Email { get; set; }

        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public required string Password { get; set; }

        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65.")]
        public int Age { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string Role { get; set; } = "User";
    }
}
