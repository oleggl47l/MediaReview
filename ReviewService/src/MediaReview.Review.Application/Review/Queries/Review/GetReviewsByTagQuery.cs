using System.ComponentModel.DataAnnotations;
using MediaReview.Review.Domain.Models;
using MediatR;

namespace MediaReview.Review.Application.Review.Queries.Review;

public class GetReviewsByTagQuery : IRequest<IEnumerable<SimpleReviewModel>>
{
    [Required] public Guid TagId { get; set; }
}