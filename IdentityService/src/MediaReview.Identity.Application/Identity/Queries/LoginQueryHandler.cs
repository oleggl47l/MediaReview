﻿using MediaReview.Identity.Domain.Exceptions;
using MediaReview.Identity.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MediaReview.Identity.Application.Identity.Queries;

public class LoginQueryHandler(
    SignInManager<Domain.Entities.User> signInManager,
    UserManager<Domain.Entities.User> userManager,
    ILogger<LoginQueryHandler> logger) : IRequestHandler<LoginQuery, LoginModel>
{
    public async Task<LoginModel> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user =
            await userManager.FindByNameAsync(request.UserName)
            ?? throw new NotFoundException($"User {request.UserName} not found.");

        if (user.AccessFailedCount > 2)
        {
            logger.LogError($"AccessFailedCount {user.AccessFailedCount}.");
            user.Active = false;
            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                logger.LogError($"\n\nFailed to update user {user.UserName} status to inactive.\n\n");
        }

        var signInResult = await signInManager.PasswordSignInAsync(request.UserName, request.Password, false, true);


        if (signInResult.IsLockedOut)
            throw new UserBlockedException(user.UserName, user.LockoutEnd.Value);
        
        if (!signInResult.Succeeded)
            throw new LoginException();

        return new LoginModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };
    }
}