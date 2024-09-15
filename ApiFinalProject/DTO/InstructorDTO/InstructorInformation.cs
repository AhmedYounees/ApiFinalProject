using ApiFinalProject.Entities;

namespace ApiFinalProject.DTO.InstructorDTO
{
    public class InstructorInformation
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; } 
        public string Address { get; set; }
        public string Name { get; set; } 
        public byte ExperienceAge { get; set; }

        public string SpecializationName { get; set; }
    }
}
