using MediaReview.Review.Domain.Entities;
using MediaReview.Review.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MediaReview.Review.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Domain.Entities.Review> Reviews { get; set; }
    public DbSet<ReviewTag> ReviewTags { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewTagConfiguration());
        modelBuilder.ApplyConfiguration(new TagConfiguration());
    }
}