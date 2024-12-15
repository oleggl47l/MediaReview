using MediaReview.Review.Domain.Entities;
using MediaReview.Review.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediaReview.Review.Infrastructure.Data.Repositories;

public class CategoryRepository(ApplicationDbContext context) : RepositoryBase<Category>(context), ICategoryRepository
{
    public async Task<bool?> CategoryExistsByNameAsync(string name)
    {
        return await DbSet.AnyAsync(c => c.Name == name);
    }
    public async Task<Category?> GetCategoryByNameAsync(string name)
    {
        return await DbSet.FirstOrDefaultAsync(c => c.Name == name);
    }
}