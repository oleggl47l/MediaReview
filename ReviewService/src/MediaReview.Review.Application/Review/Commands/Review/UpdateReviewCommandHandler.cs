using MediaReview.Review.Domain.Interfaces;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Review;

public class UpdateReviewCommandHandler(
    IReviewRepository reviewRepository,
    ICategoryRepository categoryRepository) : IRequestHandler<UpdateReviewCommand, Unit>
{
    public async Task<Unit> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await reviewRepository.GetByIdAsync(request.Id);
        if (review == null)
            throw new KeyNotFoundException($"Review with id {request.Id} not found.");
        
        if (!string.IsNullOrWhiteSpace(request.CategoryName))
        {
            var category = await categoryRepository.GetCategoryByNameAsync(request.CategoryName);
            if (category == null)
                throw new KeyNotFoundException($"Category with name {request.CategoryName} not found.");

            review.CategoryId = category.Id;
        }

        if (!string.IsNullOrWhiteSpace(request.Title))
            review.Title = request.Title;
        
        if (!string.IsNullOrWhiteSpace(request.Content))
            review.Title = request.Content;
        
        if (request.AuthorId != null && request.AuthorId != Guid.Empty)
            review.AuthorId = request.AuthorId.Value;
        
        if (request.IsPublished.HasValue)
            review.IsPublished = request.IsPublished.Value;
        
        await reviewRepository.Update(review);

        return Unit.Value;
    }
}