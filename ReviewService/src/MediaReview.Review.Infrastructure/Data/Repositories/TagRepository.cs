using MediaReview.Review.Domain.Entities;
using MediaReview.Review.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediaReview.Review.Infrastructure.Data.Repositories;

public class TagRepository(ApplicationDbContext context) : RepositoryBase<Tag>(context), ITagRepository
{
    public async Task<bool> TagExistsByNameAsync(string name)
    {
        return await DbSet.AnyAsync(t => t.Name == name);
    }
}