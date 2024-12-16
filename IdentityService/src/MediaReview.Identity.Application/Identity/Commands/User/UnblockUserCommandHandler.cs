﻿using MediaReview.Identity.Domain.Exceptions;
using MediaReview.Identity.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MediaReview.Identity.Application.Identity.Commands.User;

public class UnblockUserCommandHandler(
    UserManager<Domain.Entities.User> userManager,
    IUserService userService) : IRequestHandler<UnblockUserCommand, Unit>
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