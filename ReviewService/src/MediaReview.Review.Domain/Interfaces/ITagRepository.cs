using MediaReview.Review.Domain.Entities;

namespace MediaReview.Review.Domain.Interfaces;

public interface ITagRepository : IRepositoryBase<Tag>
{
    Task<bool> TagExistsByNameAsync(string name);
    Task<Tag?> GetTagByNameAsync(string name);
}