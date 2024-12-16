using MediaReview.Review.Domain.Interfaces;
using MediaReview.Review.Domain.Models;
using MediatR;

namespace MediaReview.Review.Application.Review.Queries.Review;

public class GetReviewsByTagQueryHandler(IReviewRepository reviewRepository, ITagRepository tagRepository) : IRequestHandler<GetReviewsByTagQuery, IEnumerable<SimpleReviewModel>>
{
    public async Task<IEnumerable<SimpleReviewModel>> Handle(GetReviewsByTagQuery request, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetByIdAsync(request.TagId);
        if (tag == null)
            throw new KeyNotFoundException("Tag not found");

        var reviews = await reviewRepository.GetReviewsByTagAsync(request.TagId);
        return reviews.Select(r=>new SimpleReviewModel
        {
            Id = r.Id,
            Title = r.Title,
            Content = r.Content,
            AuthorId = r.AuthorId
        });
    }
}