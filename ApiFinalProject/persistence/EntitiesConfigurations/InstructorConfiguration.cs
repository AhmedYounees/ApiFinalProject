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


    }
}
