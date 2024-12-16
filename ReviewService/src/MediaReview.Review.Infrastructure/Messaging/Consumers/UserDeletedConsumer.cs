using MassTransit;
using MediaReview.Review.Application.Consumers;
using MediaReview.Review.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using SharedModels;

namespace MediaReview.Review.Infrastructure.Messaging.Consumers;

public class UserDeletedConsumer(IReviewRepository reviewRepository, ILogger<UserStatusChangedEventConsumer> logger) 
    : IConsumer<UserDeletedEvent>

{
    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        logger.LogInformation("UserDeletedEventConsumer started.");

        var userDeletedEvent = context.Message;
        var userId = Guid.Parse(userDeletedEvent.UserId);

        logger.LogInformation($"Set author guid to empty fo user-related reviews. UserId: {userId}");

        var reviews = await reviewRepository.GetReviewsByUserIdAsync(userId);
        foreach (var review in reviews)
        {
            review.AuthorId = Guid.Empty;
            await reviewRepository.Update(review);
        }

        logger.LogInformation($"User reviews updated with empty user ID. UserId: {userId}");

    }
}