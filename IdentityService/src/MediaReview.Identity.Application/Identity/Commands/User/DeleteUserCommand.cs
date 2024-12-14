using MediatR;

namespace MediaReview.Identity.Application.Identity.Commands.User;

public class DeleteUserCommand : IRequest<bool>
{
    public string UserId { get; set; }
}