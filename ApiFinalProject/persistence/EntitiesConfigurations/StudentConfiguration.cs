using ApiFinalProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiFinalProject.persistence.EntitiesConfigurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {

        builder.Property(std => std.Name).HasMaxLength(100);
        builder.Property(std => std.ImageUrl).HasMaxLength(1000);
        builder.Property(std => std.Grad).HasMaxLength(100);
        builder.HasMany(std => std.StudentCourses)
                       .WithOne(cs => cs.Student)
                       .HasForeignKey(cs => cs.StudentId);
    }
}

