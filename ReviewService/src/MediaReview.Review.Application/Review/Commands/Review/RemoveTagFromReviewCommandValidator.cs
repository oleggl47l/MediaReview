using FluentValidation;
using MediaReview.Review.Domain.Interfaces;

namespace MediaReview.Review.Application.Review.Commands.Review;

public class RemoveTagFromReviewCommandValidator : AbstractValidator<RemoveTagFromReviewCommand>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly ITagRepository _tagRepository;

    public RemoveTagFromReviewCommandValidator(IReviewRepository reviewRepository, ITagRepository tagRepository)
    {
        _reviewRepository = reviewRepository;
        _tagRepository = tagRepository;

        RuleFor(x => x.ReviewId)
            .NotEmpty().WithMessage("Review ID is required.")
            .MustAsync(ReviewExists).WithMessage("Review does not exist.");

        RuleFor(x => x.TagId)
            .NotEmpty().WithMessage("Tag ID is required.")
            .MustAsync(TagExists).WithMessage("Tag does not exist.");
        
        RuleFor(x => new { x.ReviewId, x.TagId })
            .MustAsync(TagAssignedToReview)
            .WithMessage("The review does not contain this tag.");

    }

    private async Task<bool> ReviewExists(Guid reviewId, CancellationToken cancellationToken)
    {
        return await _reviewRepository.GetByIdAsync(reviewId) != null;
    }

    private async Task<bool> TagExists(Guid tagId, CancellationToken cancellationToken)
    {
        return await _tagRepository.GetByIdAsync(tagId) != null;
    }
    
    private async Task<bool> TagAssignedToReview(dynamic args, CancellationToken cancellationToken)
    {
        var reviewId = (Guid)args.ReviewId;
        var tagId = (Guid)args.TagId;

        var reviewTags = await _reviewRepository.GetTagsByReviewIdAsync(reviewId);
        return reviewTags.Any(t => t.Id == tagId);
    }
}