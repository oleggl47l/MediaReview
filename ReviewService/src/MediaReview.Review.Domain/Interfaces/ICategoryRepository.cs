using MediaReview.Review.Domain.Entities;

namespace MediaReview.Review.Domain.Interfaces;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    Task<bool?> CategoryExistsByNameAsync(string name);
    Task<Category?> GetCategoryByNameAsync(string name);
}