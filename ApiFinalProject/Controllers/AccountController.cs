using ApiFinalProject.DTO.Account;
using ApiFinalProject.Entities;
using ApiFinalProject.persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Assign role based on input
                    string role = model.Role;

                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        return BadRequest("Role does not exist.");
                    }

                    await _userManager.AddToRoleAsync(user, role);

                    if (role == "Teacher")
                    {
                        // Validate the SpecializationId
                        var specialization = await _context.Specializations.FindAsync(model.SpecializationId);
                        if (specialization == null)
                        {
                            return BadRequest("Invalid Specialization ID.");
                        }

                        var teacher = new Instructor
                        {
                            ApplicationUserId = user.Id,
                            Name = model.Name,
                            SpecializationId = model.SpecializationId
                        };

                        _context.Instructors.Add(teacher);
                    }
                    else if (role == "Student")
                    {
                        var student = new Student
                        {
                            ApplicationUserId = user.Id,
                            Name = model.Name,
                            
                        };

                        _context.Students.Add(student);
                    }

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    return Ok(new { Message = "User registered successfully", Role = role });
                }

                return BadRequest(result.Errors);
            }

            return BadRequest("Invalid model");
        }

    }
}
