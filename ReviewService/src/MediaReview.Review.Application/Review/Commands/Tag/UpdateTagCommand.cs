using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Tag;

public class UpdateTagCommand : IRequest<Unit>
{
    [Required] public Guid Id { get; set; }
    public string? Name { get; set; }
}