using ApiFinalProject.Services.dashbord;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashbordController(IDashbord _dashbord) : ControllerBase
    {
        private readonly IDashbord dashbord = _dashbord;

        [HttpGet("all")]
        public async Task< IActionResult> GetallTeacherwithCourses()
        {
                var sss=await dashbord.GetAllTeacherWithCourses();

            if(sss!=null)
            {

            return Ok(sss);
            }

            return BadRequest("empty list");



        }
    }
}
