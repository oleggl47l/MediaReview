using MediatR;

namespace MediaReview.Identity.Application.Identity.Commands.User;

public class UnblockUserCommand: IRequest<Unit>
{
    public string UserId { get; set; }
}