using MediaReview.Review.Domain.Interfaces;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Review;

public class AddTagToReviewCommandHandler(IReviewRepository reviewRepository, ITagRepository tagRepository) : IRequestHandler<AddTagToReviewCommand, Unit>
{
    public async Task<Unit> Handle(AddTagToReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await reviewRepository.GetByIdAsync(request.ReviewId);
        if (review == null)
            throw new KeyNotFoundException($"Review with id {request.ReviewId} not found.");

        var tag = await tagRepository.GetByIdAsync(request.TagId);
        if (tag == null)
            throw new KeyNotFoundException($"Tag with id {request.TagId} not found.");

        await reviewRepository.AddTagToReviewAsync(request.ReviewId, request.TagId);

        return Unit.Value;
    }
}