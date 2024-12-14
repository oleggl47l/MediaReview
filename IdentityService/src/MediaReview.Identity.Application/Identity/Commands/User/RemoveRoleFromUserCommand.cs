using MediatR;

namespace MediaReview.Identity.Application.Identity.Commands.User;

public class RemoveRoleFromUserCommand : IRequest<bool>
{
    public string UserId { get; set; }
    public string RoleName { get; set; }
}