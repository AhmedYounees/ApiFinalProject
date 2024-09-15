namespace ApiFinalProject.DTO.Account
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Student" or "Teacher"

        // Common fields
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        // Teacher-specific field
        public int SpecializationId { get; set; } // Reference to the Specialization table

        // Student-specific fields
        public string? Grade { get; set; }
    }

}
