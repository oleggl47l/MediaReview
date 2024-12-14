using MediaReview.Identity.Domain.Exceptions;
using MediaReview.Identity.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MediaReview.Identity.Application.Identity.Queries.User;

public class GetUsersByRoleIdQueryHandler(
    UserManager<Domain.Entities.User> userManager,
    RoleManager<Domain.Entities.Role> roleManager) : IRequestHandler<GetUsersByRoleIdQuery, IEnumerable<UserModel>>
{
    public async Task<IEnumerable<UserModel>> Handle(GetUsersByRoleIdQuery request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.RoleId);
        if (role == null)
            throw new NotFoundException($"Role with ID {request.RoleId} not found.");

        var usersInRole = await userManager.GetUsersInRoleAsync(role.Name);

        return usersInRole.Select(user => new UserModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Active = user.Active
        });
    }
}