using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Review;

public class UpdateReviewCommand : IRequest<Unit>
{
    [Required] public Guid Id { get; set; } 
    public string? Title { get; set; }
    public string? Content { get; set; }
    public Guid? AuthorId { get; set; }
    public string? CategoryName { get; set; }
    public bool? IsPublished { get; set; }
}