using ApiFinalProject.DTO.AuthDTO;
using ApiFinalProject.Entities;
using ApiFinalProject.persistence;
using ApiFinalProject.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await EmailExists(registerDTO.Email))
                return BadRequest("Email already exists.");

            var appUser = new ApplicationUser
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email
            };

            var createUserResult = await _userManager.CreateAsync(appUser, registerDTO.Password);
            if (!createUserResult.Succeeded)
                return BadRequest(createUserResult.Errors);

            var roleResult = await AssignRoleAndCreateEntity(registerDTO, appUser);
            if (!roleResult.Succeeded)
                return BadRequest(roleResult.Errors);

            return Ok("Account created and role assigned.");
        }

        private async Task<bool> EmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        private async Task<IdentityResult> AssignRoleAndCreateEntity(RegisterDTO registerDTO, ApplicationUser appUser)
        {
            var roleName = registerDTO.Role switch
            {
                1 => "Admin",
                2 => "Instructor",
                3 => "Student",
                _ => throw new ArgumentException("Invalid role.")
            };

            await EnsureRoleExists(roleName);

            if (registerDTO.Role == 2)
            {
                await AddInstructor(registerDTO, appUser);
            }
            else if (registerDTO.Role == 3)
            {
                await AddStudent(registerDTO, appUser);
            }

            return await _userManager.AddToRoleAsync(appUser, roleName);
        }

        private async Task AddInstructor(RegisterDTO registerDTO, ApplicationUser appUser)
        {
            var instructor = new Instructor
            {
                Name = registerDTO.Name,
                ApplicationUserId = appUser.Id,
                SpecializationId = registerDTO.SpecializationId
            };
            await _context.Instructors.AddAsync(instructor);
            await _context.SaveChangesAsync();
        }

        private async Task AddStudent(RegisterDTO registerDTO, ApplicationUser appUser)
        {
            var student = new Student
            {
                Name = registerDTO.Name,
                ApplicationUserId = appUser.Id
            };
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        private async Task EnsureRoleExists(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleResult.Succeeded)
                    throw new InvalidOperationException($"Failed to create role: {roleName}");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userFromDb = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (userFromDb == null || !await _userManager.CheckPasswordAsync(userFromDb, loginDTO.Password))
                return BadRequest(new { error = "Invalid email or password." });

            var token = GenerateJwtToken(userFromDb);

            return Ok(new { Token = token, Expiration = DateTime.Now.AddMinutes(15) });
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = GenerateClaims(user).Result;

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private async Task<IEnumerable<Claim>> GenerateClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }
    }
}
