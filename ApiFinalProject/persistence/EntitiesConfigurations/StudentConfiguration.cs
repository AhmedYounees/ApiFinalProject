using ApiFinalProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ApiFinalProject.persistence.EntitiesConfigurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {

        builder.Property(std => std.Name).HasMaxLength(100);
        builder.Property(std => std.ImageUrl).HasMaxLength(1000);
       // builder.Property(std => std.Grad).HasMaxLength(100);
        builder.HasMany(std => std.StudentCourses)
                       .WithOne(cs => cs.Student)
                       .HasForeignKey(cs => cs.StudentId);
        builder
           .HasOne(s => s.ApplicationUser) // A student has one ApplicationUser
           .WithOne() // ApplicationUser has one Student
           .HasForeignKey<Student>(s => s.ApplicationUserId) // Use ApplicationUserId as foreign key in Student
           .IsRequired();
    }
}

