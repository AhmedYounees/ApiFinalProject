using ApiFinalProject.DTO.Category;
using ApiFinalProject.Entities;
using ApiFinalProject.persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ApplicationDbContext _context) : ControllerBase
    {
        private readonly ApplicationDbContext context = _context;
        [HttpPost]
        public IActionResult add(CategoryDTO category)
        {

              
            if (category == null)
            {
                return NotFound();
            }

            Category category1 = new Category();
            category1.Name = category.Name;
            
             context.Categories.Add(category1);
            context.SaveChanges();

            return Ok("adedded sucsessfuuly");





        }

    }
}
