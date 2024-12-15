using System.ComponentModel.DataAnnotations;
using MediaReview.Review.Domain.Models;
using MediatR;

namespace MediaReview.Review.Application.Review.Queries.Category;

public class GetCategoryByIdQuery : IRequest<CategoryModel>
{
    [Required] public Guid Id { get; init; }
}