using ApiFinalProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiFinalProject.persistence.EntitiesConfigurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{

    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasMany(cat => cat.Courses)
                    .WithOne(crs => crs.Category)
                    .HasForeignKey(crs => crs.CategoryId);

        builder.HasOne(cat => cat.Specialization)
                    .WithMany(spliz => spliz.Categories)
                    .HasForeignKey(cat => cat.SpecializationId);
    }
}
