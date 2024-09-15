
using ApiFinalProject.Entities;
using ApiFinalProject.persistence;
using ApiFinalProject.Services.Chapter;
using ApiFinalProject.Services.Course;
using ApiFinalProject.Services.dashbord;
using ApiFinalProject.Services.Video;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiFinalProject
{
    public class Program
    {
        public static void Main(string[] args)
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
            // Add IWebHostEnvironment
            //builder.Services.AddSingleton<IWebHostEnvironment,IWebHostEnvironment>();
            builder.Services.AddScoped<IChapterService, ChapterService>();
            builder.Services.AddScoped<ICourseService, CourseService>();
          //  builder.Services.AddScoped<IDashbord, DashbordService>();
            builder.Services.AddScoped<IVideoService, VideoService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // Enable serving of static files (e.g., for videos)
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }
    }
}
