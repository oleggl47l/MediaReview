using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MediaReview.Identity.Application.Identity.Commands.User;

public class CreateUserCommandHandler(
    UserManager<Domain.Entities.User> userManager,
    RoleManager<Domain.Entities.Role> roleManager) : IRequestHandler<CreateUserCommand, Unit>
{
    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.UserName) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ArgumentNullException("Username, Email, and Password are required.");
        }

        var existingEmail = await userManager.FindByEmailAsync(request.Email);
        var existingUsername = await userManager.FindByNameAsync(request.UserName);
        if (existingEmail != null)
            throw new Exception($"User with email {request.Email} already exists.");
        if (existingUsername != null)
            throw new Exception($"User with username {request.UserName} already exists.");

        var user = new global::MediaReview.Identity.Domain.Entities.User
        {
            UserName = request.UserName,
            Email = request.Email,
        };

        var roles = await GetRolesByIds(request.RoleIds);
        if (roles == null || !roles.Any())
            throw new Exception("One or more roles do not exist.");

        var createResult = await userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
        {
            var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
            throw new Exception($"Failed to create user: {errors}");
        }

        await AddRolesToUserAsync(user, roles);

        return Unit.Value;
    }

    private async Task<List<global::MediaReview.Identity.Domain.Entities.Role>> GetRolesByIds(IEnumerable<string> roleIds)
    {
        var roles = new List<global::MediaReview.Identity.Domain.Entities.Role>();

        foreach (var roleId in roleIds)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
                throw new NullReferenceException($"Role with ID {roleId} does not exist.");

            roles.Add(role);
        }

        return roles;
    }

    private async Task AddRolesToUserAsync(global::MediaReview.Identity.Domain.Entities.User user, List<global::MediaReview.Identity.Domain.Entities.Role> roles)
    {
        foreach (var role in roles)
        {
            if (role.Name != null)
            {
                var addRoleResult = await userManager.AddToRoleAsync(user, role.Name);
                if (!addRoleResult.Succeeded)
                    throw new Exception($"Failed to add role {role.Name} to user {user.UserName}");
            }
        }
    }
}