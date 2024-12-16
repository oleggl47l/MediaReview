using MediaReview.Review.Domain.Entities;
using MediaReview.Review.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediaReview.Review.Infrastructure.Data.Repositories;

public class ReviewRepository(ApplicationDbContext context)
    : RepositoryBase<Domain.Entities.Review>(context), IReviewRepository
{
    public async Task<IEnumerable<Domain.Entities.Review>> GetReviewsByCategoryAsync(Guid categoryId)
    {
        return await DbSet
            .Where(r => r.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Review>> GetReviewsByTagAsync(Guid tagId)
    {
        return await Context.Set<ReviewTag>()
            .Where(rt => rt.TagId == tagId)
            .Select(rt => rt.Review)
            .ToListAsync();
    }

    public async Task AddTagToReviewAsync(Guid reviewId, Guid tagId)
    {
        var review = await GetByIdAsync(reviewId);
        if (review == null) return;

        var tag = await Context.Set<Tag>().FindAsync(tagId);
        if (tag == null) return;

        var reviewTag = new ReviewTag
        {
            ReviewId = reviewId,
            TagId = tagId
        };

        await Context.Set<ReviewTag>().AddAsync(reviewTag);
        await Context.SaveChangesAsync();
    }

    public async Task RemoveTagFromReviewAsync(Guid reviewId, Guid tagId)
    {
        var reviewTag = await Context.Set<ReviewTag>()
            .FirstOrDefaultAsync(rt => rt.ReviewId == reviewId && rt.TagId == tagId);
        if (reviewTag == null) return;

        Context.Set<ReviewTag>().Remove(reviewTag);
        await Context.SaveChangesAsync();
    }

    public async Task<Domain.Entities.Review?> GetReviewWithCategoryAndTagsAsync(Guid reviewId)
    {
        return await DbSet
            .Include(r => r.Category)
            .Include(r => r.ReviewTags)
            .ThenInclude(rt => rt.Tag)
            .FirstOrDefaultAsync(r => r.Id == reviewId);
    }

    public async Task<IEnumerable<Domain.Entities.Review>> GetAllReviewsWithCategoryAndTagsAsync()
    {
        return await DbSet
            .Include(r => r.Category)
            .Include(r => r.ReviewTags)
            .ThenInclude(rt => rt.Tag)
            .ToListAsync();
    }

    public async Task<IEnumerable<Tag>> GetTagsByReviewIdAsync(Guid reviewId)
    {
        return await Context.Set<ReviewTag>()
            .Where(rt => rt.ReviewId == reviewId)
            .Select(rt => rt.Tag)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Domain.Entities.Review>> GetReviewsByUserIdAsync(Guid userId)
    {
        return await DbSet
            .Where(r => r.AuthorId == userId)
            .ToListAsync();
    }
}