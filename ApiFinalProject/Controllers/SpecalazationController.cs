using ApiFinalProject.DTO.Specalization;
using ApiFinalProject.Entities;
using ApiFinalProject.Services.Instructors;
using ApiFinalProject.Services.Specalazation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecalazationController : ControllerBase
    {
        
        ISpecalazation ispecalazation;
        public SpecalazationController(ISpecalazation specalazation)
        {
            
            ispecalazation = specalazation;

        }

        [HttpPost]
        public IActionResult addSpe(specalizationRequst s1)
        {
            Specialization specialization=new Specialization();
            specialization.Name = s1.name;
            ispecalazation.add(specialization);
            ispecalazation.Save();
            return Created();
        }
    }
}
