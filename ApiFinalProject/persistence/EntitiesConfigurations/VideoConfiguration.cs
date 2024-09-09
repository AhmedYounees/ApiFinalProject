using ApiFinalProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiFinalProject.persistence.EntitiesConfigurations;

public class VideoConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {

        builder.HasOne(v => v.Chapter)
            .WithMany(ch => ch.Videos)
            .HasForeignKey(v => v.ChapterID);

    }
}
