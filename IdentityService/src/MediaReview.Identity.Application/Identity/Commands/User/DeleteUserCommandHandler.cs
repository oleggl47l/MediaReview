using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MediaReview.Identity.Application.Identity.Commands.User;

public class DeleteUserCommandHandler(UserManager<Domain.Entities.User> userManager)
    : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);

        if (user == null)
            return false;
        
        var result = await userManager.DeleteAsync(user);
        return result.Succeeded;
    }
}