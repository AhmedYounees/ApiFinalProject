using System.ComponentModel.DataAnnotations;

namespace ApiFinalProject.DTO.AuthDTO
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int SpecializationId { get; set; }
        [Required]
        public int  Role { get; set; }

    }
}
