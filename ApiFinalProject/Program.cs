
using ApiFinalProject.Entities;
using ApiFinalProject.persistence;
using ApiFinalProject.Services.dashbord;
using ApiFinalProject.Services.Instructors;
using ApiFinalProject.Services.Specalazation;
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
            builder.Services.AddScoped<IInstructor, InstructorService>();
            builder.Services.AddScoped<ISpecalazation, SpecalazationService>();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IInstructor, InstructorService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }
    }
}
