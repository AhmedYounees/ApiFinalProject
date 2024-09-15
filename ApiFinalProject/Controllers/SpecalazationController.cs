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
        public IActionResult addSpe(Specialization s1)
        {
            ispecalazation.add(s1);
            ispecalazation.Save();
            return Created();
        }
    }
}
