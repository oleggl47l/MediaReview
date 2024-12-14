using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MediaReview.Identity.Application.Identity.Commands.User;

public class UpdateUserCommandHandler(
    UserManager<Domain.Entities.User> userManager) : IRequestHandler<UpdateUserCommand, bool>
{
    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);

        if (user == null)
            throw new NullReferenceException(nameof(user));

        if (!string.IsNullOrEmpty(request.UserName))
            user.UserName = request.UserName;

        if (!string.IsNullOrEmpty(request.Email))
            user.Email = request.Email;
        
        if (!string.IsNullOrEmpty(request.OldPassword) && !string.IsNullOrEmpty(request.NewPassword))
        {
            var checkPassword = await userManager.CheckPasswordAsync(user, request.OldPassword);
            if (!checkPassword)
            {
                throw new UnauthorizedAccessException("Old password is incorrect.");
            }

            var changePasswordResult = await userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                throw new Exception($"Failed to update the password: {string.Join(", ", changePasswordResult.Errors.Select(e => e.Description))}");
            }
        }

        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            throw new InvalidOperationException("Failed to update user.");

        return true;
    }
}