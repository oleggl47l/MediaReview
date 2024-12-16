using MediaReview.Review.Domain.Interfaces;
using MediaReview.Review.Domain.Models;
using MediatR;

namespace MediaReview.Review.Application.Review.Queries.Review;

public class GetReviewsByCategoryQueryHandler(IReviewRepository reviewRepository, ICategoryRepository categoryRepository) : IRequestHandler<GetReviewsByCategoryQuery, IEnumerable<SimpleReviewModel>>
{
    public async Task<IEnumerable<SimpleReviewModel>> Handle(GetReviewsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null)
            throw new KeyNotFoundException("Category not found");
        
        var reviews = await reviewRepository.GetReviewsByCategoryAsync(category.Id);
        return reviews.Select(r=>new SimpleReviewModel
        {
            Id = r.Id,
            Title = r.Title,
            Content = r.Content,
            AuthorId = r.AuthorId
        });
    }
}