using MediaReview.Identity.Application.Services;
using MediaReview.Identity.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MediaReview.Identity.Application.Identity.Commands.User;

public class UnblockUserCommandHandler(
    UserManager<Domain.Entities.User> userManager,
    UserService userService) : IRequestHandler<UnblockUserCommand, Unit>
{
    public async Task<Unit> Handle(UnblockUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user == null)
            throw new NotFoundException("User not found");
        
        await userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow);

        user.Active = true;
        await userManager.UpdateAsync(user);

        await userService.NotifyUserStatusChanged(user.Id);

        return Unit.Value;
    }
}