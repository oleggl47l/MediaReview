using System.Security.Claims;
using MediaReview.Review.Domain.Entities;
using MediaReview.Review.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace MediaReview.Review.Application.Review.Commands.Review;

public class CreateReviewCommandHandler(
    IReviewRepository reviewRepository,
    ICategoryRepository categoryRepository,
    ITagRepository tagRepository,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateReviewCommand, Unit>
{
    public async Task<Unit> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            throw new UnauthorizedAccessException("User is not authorized.");

        var userId = Guid.Parse(userIdClaim.Value);

        var category = await categoryRepository.GetCategoryByNameAsync(request.CategoryName);
        if (category == null)
            throw new KeyNotFoundException($"Category {category} not found");

        var review = new Domain.Entities.Review
        {
            Title = request.Title,
            Content = request.Content,
            AuthorId = userId,
            CategoryId = category.Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsPublished = true
        };

        var existingTags = await tagRepository.GetAllAsync();
        var reviewTags = new List<ReviewTag>();

        if (request.TagNames != null && request.TagNames.Any())
        {
            foreach (var tagName in request.TagNames)
            {
                var tag = existingTags.FirstOrDefault(t => t.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase));
                if (tag != null)
                    reviewTags.Add(new ReviewTag { TagId = tag.Id, Review = review });
                else
                {
                    tag = new Domain.Entities.Tag { Name = tagName };
                    await tagRepository.AddAsync(tag);
                    reviewTags.Add(new ReviewTag { TagId = tag.Id, Review = review });
                }
            }
        }

        review.ReviewTags = reviewTags;

        await reviewRepository.AddAsync(review);

        return Unit.Value;
    }
}