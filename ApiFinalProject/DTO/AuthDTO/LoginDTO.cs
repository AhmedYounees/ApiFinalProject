using System.ComponentModel.DataAnnotations;

namespace ApiFinalProject.DTO.AuthDTO
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
