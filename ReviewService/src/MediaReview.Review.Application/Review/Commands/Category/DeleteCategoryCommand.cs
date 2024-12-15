using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Category;

public class DeleteCategoryCommand : IRequest<Unit>
{
    [Required] public Guid Id { get; set; }
}