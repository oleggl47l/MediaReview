using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Tag;

public class CreateTagCommand : IRequest<Unit>
{
    public string Name { get; set; }
}