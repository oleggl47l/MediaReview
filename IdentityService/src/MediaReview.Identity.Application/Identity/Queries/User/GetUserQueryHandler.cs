using MediaReview.Identity.Domain.Exceptions;
using MediaReview.Identity.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MediaReview.Identity.Application.Identity.Queries.User;

public class GetUserQueryHandler(
    UserManager<Domain.Entities.User> userManager) : IRequestHandler<GetUserQuery, UserModel>
{
    public async Task<UserModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);

        if (user == null)
            throw new NotFoundException($"User with id {request.UserId} not found");

        var userRoles = await userManager
            .GetRolesAsync(user);

        return new UserModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Roles = userRoles.ToList(),
            Active = user.Active
        };
    }
}