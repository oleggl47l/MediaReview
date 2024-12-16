using MediaReview.Review.Domain.Interfaces;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Review;

public class RemoveTagFromReviewCommandHandler(IReviewRepository reviewRepository, ITagRepository tagRepository)
    : IRequestHandler<RemoveTagFromReviewCommand, Unit>
{
    public async Task<Unit> Handle(RemoveTagFromReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await reviewRepository.GetByIdAsync(request.ReviewId);
        if (review == null)
            throw new KeyNotFoundException($"Review with id {request.ReviewId} not found.");

        var tag = await tagRepository.GetByIdAsync(request.TagId);
        if (tag == null)
            throw new KeyNotFoundException($"Tag with id {request.TagId} not found.");

        await reviewRepository.RemoveTagFromReviewAsync(request.ReviewId, request.TagId);

        return Unit.Value;
    }
}