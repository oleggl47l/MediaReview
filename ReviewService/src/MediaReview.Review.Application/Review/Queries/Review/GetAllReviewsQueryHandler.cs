using MediaReview.Review.Domain.Interfaces;
using MediaReview.Review.Domain.Models;
using MediatR;

namespace MediaReview.Review.Application.Review.Queries.Review;

public class GetAllReviewsQueryHandler(IReviewRepository reviewRepository)
    : IRequestHandler<GetAllReviewsQuery, IEnumerable<ReviewModel>>
{
    public async Task<IEnumerable<ReviewModel>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await reviewRepository.GetAllReviewsWithCategoryAndTagsAsync();
        return reviews.Select(review => new ReviewModel
        {
            Id = review.Id,
            Title = review.Title,
            Content = review.Content,
            AuthorId = review.AuthorId,
            CreatedAt = review.CreatedAt,
            UpdatedAt = review.UpdatedAt,
            IsPublished = review.IsPublished,
            Category = review.Category?.Name ?? string.Empty,
            TagNames = review.ReviewTags?.Select(rt => rt.Tag.Name).ToList()
        }).ToList();
    }
}