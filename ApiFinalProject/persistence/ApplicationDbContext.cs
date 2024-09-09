using ApiFinalProject.Entities;
using ApiFinalProject.persistence.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ApiFinalProject.persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; } 
    public DbSet<Chapter> Chapters { get; set; } 
    public DbSet<Course> Courses { get; set; } 
    public DbSet<CourseInstructor> CourseInstructors { get; set; } 
    public DbSet<CourseStudent> CourseStudents { get; set; }
    public DbSet<Instructor> Instructors { get; set; } 
    public DbSet<Review> Reviews { get; set; } 
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Video> Videos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define composite primary key
       modelBuilder.Entity<CourseStudent>().HasKey(cs => new { cs.CourseId, cs.StudentId });

        // Define relationships
       modelBuilder.Entity<CourseStudent>().HasOne(cs => cs.Student)
               .WithMany(s => s.StudentCourses)
               .HasForeignKey(cs => cs.StudentId);

        modelBuilder.Entity<CourseStudent>().HasOne(cs => cs.Course)
               .WithMany(c => c.StudentCourses)
               .HasForeignKey(cs => cs.CourseId);
        // Apply configurations for each entity
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ChapterConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
       // modelBuilder.ApplyConfiguration(new CourseStudentConfiguration());
        modelBuilder.ApplyConfiguration(new InstructorConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        modelBuilder.ApplyConfiguration(new SpecializationConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new VideoConfiguration());

        // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
