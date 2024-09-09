using ApiFinalProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiFinalProject.persistence.EntitiesConfigurations;

public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.Property(spliz => spliz.Name).IsRequired();
        builder.Property(spliz => spliz.Name).HasMaxLength(100);

        builder.HasMany(spliz => spliz.Instructors)
            .WithOne(inst => inst.Specialization)
            .HasForeignKey(inst => inst.SpecializationId);

        builder.HasMany(spliz => spliz.Categories)
            .WithOne(cat => cat.Specialization)
            .HasForeignKey(cat => cat.SpecializationId);
    }
}
