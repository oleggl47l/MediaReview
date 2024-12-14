﻿using MediaReview.Identity.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MediaReview.Identity.Application.Identity.Commands.Role;

public class DeleteRoleCommandHandler(RoleManager<Domain.Entities.Role> roleManager, UserManager<Domain.Entities.User> userManager)
    : IRequestHandler<DeleteRoleCommand, Unit>
{
    public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.RoleId);
        if (role == null)
            throw new NotFoundException($"Role with id {request.RoleId} does not exist");
        

        if (role.Name != null)
        {
            var usersInRole = await userManager.GetUsersInRoleAsync(role.Name);
            if (usersInRole.Any())
                throw new ConflictException("Impossible to delete a role because there are users who have this role.");
        }

        var result = await roleManager.DeleteAsync(role);
        if (!result.Succeeded)
            throw new InvalidOperationException("Failed to delete role");

        return Unit.Value;
    }
}