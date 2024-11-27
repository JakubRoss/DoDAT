using System.ComponentModel.DataAnnotations;

namespace DoDAT.Domain.Models
{
    public class UserDto
    {
        [Required(ErrorMessage = "Login is required.")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Login cannot be longer than 10 characters.")]
        [RegularExpression(@"^\S+$", ErrorMessage = "Login cannot contain white spaces.")]
        public string Login { get; set; } = default!;
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = default!;
    }
}
