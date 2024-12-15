using MediaReview.Review.Domain.Interfaces;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Review;

public class DeleteReviewCommandHandler(IReviewRepository reviewRepository) : IRequestHandler<DeleteReviewCommand, Unit>
{
    public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await reviewRepository.GetByIdAsync(request.Id);
        if (review == null)
            throw new KeyNotFoundException($"Review with id {request.Id} not found.");

        await reviewRepository.Remove(review.Id);
        return Unit.Value;
    }
}