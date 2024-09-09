using ApiFinalProject.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiFinalProject.persistence.EntitiesConfigurations;

public class CourseStudentConfiguration
{
    public void Configure(EntityTypeBuilder<CourseStudent> builder)
    {
        // Define composite primary key
        builder.HasKey(cs => new { cs.CourseId, cs.StudentId });

        // Define relationships
        builder.HasOne(cs => cs.Student)
               .WithMany(s => s.StudentCourses)
               .HasForeignKey(cs => cs.StudentId);

        builder.HasOne(cs => cs.Course)
               .WithMany(c => c.StudentCourses)
               .HasForeignKey(cs => cs.CourseId);
    }
}
