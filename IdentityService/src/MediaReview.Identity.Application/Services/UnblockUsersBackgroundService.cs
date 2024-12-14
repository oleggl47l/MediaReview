﻿using MediaReview.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MediaReview.Identity.Application.Services;

public class UnblockUsersBackgroundService(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<UnblockUsersBackgroundService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceScopeFactory.CreateScope();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                var users = userManager.Users.Where(u => !u.Active).ToList();

                foreach (var user in users)
                {
                    var lockoutEnd = await userManager.GetLockoutEndDateAsync(user);

                    if (lockoutEnd <= DateTime.UtcNow)
                    {
                        user.Active = true;
                        await userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow);
                        await userManager.UpdateAsync(user);
                        logger.LogInformation($"User {user.UserName} has been unlocked.");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while unlocking users.");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}