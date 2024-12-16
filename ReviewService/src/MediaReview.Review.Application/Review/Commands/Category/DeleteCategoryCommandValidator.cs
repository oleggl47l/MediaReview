using FluentValidation;
using MediaReview.Review.Domain.Interfaces;

namespace MediaReview.Review.Application.Review.Commands.Category;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    private readonly IReviewRepository _reviewRepository;

    public DeleteCategoryCommandValidator(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty");

        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) => !await CategoryHasReviewsAsync(id, cancellationToken))
            .WithMessage("Category cannot be deleted because it has associated reviews.");
    }

    private async Task<bool> CategoryHasReviewsAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var reviews = await _reviewRepository.GetReviewsByCategoryAsync(categoryId);
        return reviews.Any();
    }
}