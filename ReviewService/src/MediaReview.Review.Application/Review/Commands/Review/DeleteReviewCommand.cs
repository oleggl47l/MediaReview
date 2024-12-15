using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Review;

public class DeleteReviewCommand : IRequest<Unit>
{
    [Required] public Guid Id { get; set; }
}