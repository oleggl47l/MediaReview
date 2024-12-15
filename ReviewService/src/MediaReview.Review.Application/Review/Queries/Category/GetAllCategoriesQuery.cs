using MediaReview.Review.Domain.Models;
using MediatR;

namespace MediaReview.Review.Application.Review.Queries.Category;

public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryModel>>;