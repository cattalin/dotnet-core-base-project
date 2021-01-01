using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class AuthLoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
