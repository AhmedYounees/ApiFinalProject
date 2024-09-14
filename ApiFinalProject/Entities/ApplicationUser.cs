using Microsoft.AspNetCore.Identity;

namespace ApiFinalProject.Entities
{
    public class ApplicationUser:IdentityUser
    {

        public Student Student { get; set; }
        public Instructor instructor { get; set; }

    }
}
