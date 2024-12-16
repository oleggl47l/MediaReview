﻿using MassTransit;
using MediaReview.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using SharedModels;

namespace MediaReview.Identity.Application.Services;

public class UserService(IPublishEndpoint publishEndpoint, UserManager<User> userManager)
{
    public async Task NotifyUserStatusChanged(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user != null)
        {
            var userStatusChangedEvent = new UserStatusChangedEvent
            {
                UserId = user.Id,
                Active = user.Active,
                ChangedAt = DateTime.UtcNow
            };

            await publishEndpoint.Publish(userStatusChangedEvent);
        }
    }
    
    public async Task NotifyUserDeleted(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            var userDeletedEvent = new UserDeletedEvent
            {
                UserId = userId,
                DeletedAt = DateTime.UtcNow
            };
            await publishEndpoint.Publish(userDeletedEvent);
        }
    }
}
