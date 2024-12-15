using MediaReview.Review.Domain.Models;
using MediatR;

namespace MediaReview.Review.Application.Review.Queries.Review;

public class GetAllReviewsQuery : IRequest<IEnumerable<ReviewModel>>;