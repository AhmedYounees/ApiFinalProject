
using ApiFinalProject.Entities;
using ApiFinalProject.persistence;
using ApiFinalProject.Services.Chapter;
using ApiFinalProject.Services.Course;
using ApiFinalProject.Services.dashbord;
using ApiFinalProject.Services.Video;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiFinalProject
{
    public class Program
    {

        // Method to ensure roles exist in the database
      static  async Task EnsureRolesExist(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Student"))
            {
                await roleManager.CreateAsync(new IdentityRole("Student"));
            }
            if (!await roleManager.RoleExistsAsync("Teacher"))
            {
                await roleManager.CreateAsync(new IdentityRole("Teacher"));
            }
        }


        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>

            options.UseSqlServer(builder.Configuration.GetConnectionString("cs"))
            ); ;
           builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllers();
            builder.Services.AddScoped<IDashbord,DashbordService>();
            builder.Services.AddScoped<IChapterService, ChapterService>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            
            builder.Services.AddScoped<IVideoService, VideoService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //allow cors policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            var app = builder.Build();
            //add two role student and teacher
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await EnsureRolesExist(roleManager);
                }
                catch (Exception ex)
                {
                    // Handle exceptions as needed, such as logging
                    Console.WriteLine($"Error creating roles: {ex.Message}");
                }
            }
            //builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            //builder.Logging.AddConsole();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");
            // Enable serving of static files (e.g., for videos)
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }
    }
}
