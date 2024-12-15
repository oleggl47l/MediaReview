using MediaReview.Review.Domain.Interfaces;
using MediaReview.Review.Domain.Models;
using MediatR;

namespace MediaReview.Review.Application.Review.Queries.Review;

public class GetReviewByIdQueryHandler(IReviewRepository reviewRepository)
    : IRequestHandler<GetReviewByIdQuery, ReviewModel>
{
    public async Task<ReviewModel> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var review = await reviewRepository.GetReviewWithCategoryAndTagsAsync(request.Id);
        if (review == null)
        {
            throw new KeyNotFoundException("Review not found");
        }

        return new ReviewModel
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
        };
    }
}
