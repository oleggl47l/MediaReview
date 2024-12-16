using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Review;

public class CreateReviewCommand : IRequest<Unit>
{
    [Required] public string Title { get; set; }
    [Required] public string Content { get; set; }
    [Required] public string CategoryName { get; set; }
    public List<string>? TagNames { get; set; }
}