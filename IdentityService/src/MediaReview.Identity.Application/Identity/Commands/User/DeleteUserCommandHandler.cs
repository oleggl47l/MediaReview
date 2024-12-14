using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MediaReview.Identity.Application.Identity.Commands.User;

public class DeleteUserCommandHandler(UserManager<Domain.Entities.User> userManager)
    : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);

        if (user == null)
            throw new NullReferenceException($"Username `{nameof(user)}` does not exist.");

        var result = await userManager.DeleteAsync(user);
        return Unit.Value;
    }
}