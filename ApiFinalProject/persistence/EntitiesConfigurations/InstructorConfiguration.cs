using ApiFinalProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiFinalProject.persistence.EntitiesConfigurations;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.Property(inst => inst.Address).HasMaxLength(100);
        builder.Property(inst => inst.Name).HasMaxLength(200);

        builder.HasOne(inst => inst.Specialization)
            .WithMany(spliz => spliz.Instructors)
            .HasForeignKey(inst => inst.SpecializationId);

        builder
          .HasOne(s => s.ApplicationUser) // A student has one ApplicationUser
          .WithOne(u => u.instructor) // ApplicationUser has one Student
          .HasForeignKey<Instructor>(s => s.ApplicationUserId) // Use ApplicationUserId as foreign key in Student
          .IsRequired();



    }
}
