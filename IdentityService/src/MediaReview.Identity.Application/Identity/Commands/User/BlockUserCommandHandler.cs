﻿using MediaReview.Identity.Domain.Exceptions;
using MediaReview.Identity.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MediaReview.Identity.Application.Identity.Commands.User;

public class BlockUserCommandHandler(
    UserManager<Domain.Entities.User> userManager,
    IUserService userService) : IRequestHandler<BlockUserCommand, Unit>
{
    public async Task<Unit> Handle(BlockUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user == null)
            throw new NotFoundException($"User with ID {request.UserId} not found");

        var lockoutEndTime = DateTime.UtcNow.AddMinutes(request.TimeInMinutes);

        user.Active = false;
        await userManager.UpdateAsync(user);

        await userManager.SetLockoutEnabledAsync(user, true);
        await userManager.SetLockoutEndDateAsync(user, lockoutEndTime);

        await userService.NotifyUserStatusChanged(user.Id);

        return Unit.Value;
    }
}