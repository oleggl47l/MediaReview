using MediatR;

namespace MediaReview.Identity.Application.Identity.Commands.User;

public class BlockUserCommand : IRequest<Unit>
{
    public string UserId { get; set; }
    public int TimeInMinutes { get; set; }
}