using MediaReview.Review.Domain.Entities;

namespace MediaReview.Review.Domain.Interfaces;

public interface IReviewRepository : IRepositoryBase<Entities.Review>
{
    Task<IEnumerable<Entities.Review>> GetReviewsByCategoryAsync(Guid categoryId);

    Task<IEnumerable<Entities.Review>> GetReviewsByTagAsync(Guid tagId);

    Task AddTagToReviewAsync(Guid reviewId, Guid tagId);

    Task RemoveTagFromReviewAsync(Guid reviewId, Guid tagId);
    Task<Domain.Entities.Review?> GetReviewWithCategoryAndTagsAsync(Guid reviewId);
    Task<IEnumerable<Domain.Entities.Review>> GetAllReviewsWithCategoryAndTagsAsync();
    Task<IEnumerable<Tag>> GetTagsByReviewIdAsync(Guid reviewId);
}