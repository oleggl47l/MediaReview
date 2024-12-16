using MediaReview.Review.Domain.Interfaces;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Category;

public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IReviewRepository reviewRepository) : IRequestHandler<DeleteCategoryCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id);
        if (category == null)
            throw new KeyNotFoundException($"Category with id {request.Id} not found.");
        
        var reviews = await reviewRepository.GetReviewsByCategoryAsync(request.Id);
        if (reviews.Any()) 
            throw new InvalidOperationException("Cannot delete category because there are reviews associated with it.");
        
        await categoryRepository.Remove(request.Id);
        return Unit.Value;
    }
}