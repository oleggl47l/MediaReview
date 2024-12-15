using FluentValidation;
using MediaReview.Review.Domain.Interfaces;

namespace MediaReview.Review.Application.Review.Commands.Review;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateReviewCommandValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title should not exceed 200 characters.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.");

        RuleFor(x => x.CategoryName)
            .NotEmpty().WithMessage("Category is required.")
            .MustAsync(CategoryExists).WithMessage("Category does not exist.");

        RuleForEach(x => x.TagNames)
            .NotEmpty().WithMessage("Tag name cannot be empty.")
            .MaximumLength(100).WithMessage("Tag name should not exceed 100 characters.")
            .When(x => x.TagNames != null && x.TagNames.Any());
    }

    private async Task<bool> CategoryExists(string categoryName, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryByNameAsync(categoryName);
        return category != null;
    }
}