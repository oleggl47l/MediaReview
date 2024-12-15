using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Category;

public class UpdateCategoryCommand : IRequest<Unit>
{
    [Required] public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}