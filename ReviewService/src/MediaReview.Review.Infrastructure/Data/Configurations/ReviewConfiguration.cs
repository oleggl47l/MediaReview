using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaReview.Review.Infrastructure.Data.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Domain.Entities.Review>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Review> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Title).IsRequired().HasMaxLength(200);
        builder.Property(r => r.Content).IsRequired();
        builder.Property(r => r.CreatedAt).IsRequired();
        builder.Property(r => r.UpdatedAt).IsRequired();

        builder.HasOne(r => r.Category)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}