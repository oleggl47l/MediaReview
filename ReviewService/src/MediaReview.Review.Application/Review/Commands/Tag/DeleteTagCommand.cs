using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Tag;

public class DeleteTagCommand : IRequest<Unit>
{
    [Required] public Guid Id { get; init; }
}