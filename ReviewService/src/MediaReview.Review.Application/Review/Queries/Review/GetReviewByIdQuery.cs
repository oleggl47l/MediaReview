using System.ComponentModel.DataAnnotations;
using MediaReview.Review.Domain.Models;
using MediatR;

namespace MediaReview.Review.Application.Review.Queries.Review;

public class GetReviewByIdQuery : IRequest<ReviewModel>
{
    [Required] public Guid Id { get; set; }
}