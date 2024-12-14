﻿using MediaReview.Identity.Domain.Exceptions;
using MediaReview.Identity.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MediaReview.Identity.Application.Identity.Commands.Role;

public class UpdateRoleCommandHandler(RoleManager<Domain.Entities.Role> roleManager)
    : IRequestHandler<UpdateRoleCommand, RoleModel>
{
    public async Task<RoleModel> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.RoleId);
        if (role == null)
            throw new NotFoundException($"Role with id: {request.RoleId} does not exist");

        if (!string.IsNullOrEmpty(request.Name))
            role.Name = request.Name;

        if (request.IsActive.HasValue)
            role.IsActive = request.IsActive.Value;

        var result = await roleManager.UpdateAsync(role);
        if (!result.Succeeded)
            throw new InvalidOperationException("Failed to update role");

        return new RoleModel(role.Id, role.Name, role.IsActive);
    }
}