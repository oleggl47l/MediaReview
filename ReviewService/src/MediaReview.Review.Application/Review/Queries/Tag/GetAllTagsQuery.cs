using MediaReview.Review.Domain.Models;
using MediatR;

namespace MediaReview.Review.Application.Review.Queries.Tag;

public class GetAllTagsQuery : IRequest<IEnumerable<TagModel>>;