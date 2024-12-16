using MassTransit;
using MediaReview.Review.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using SharedModels;

namespace MediaReview.Review.Application.Consumers;

public class UserStatusChangedEventConsumer(IReviewRepository reviewRepository, ILogger<UserStatusChangedEvent> logger) : IConsumer<UserStatusChangedEvent>
{
    public async Task Consume(ConsumeContext<UserStatusChangedEvent> context)
    {
        logger.LogInformation("UserStatusChangedEventConsumer started.");
        
        var userStatusChangedEvent = context.Message;

        if (!userStatusChangedEvent.Active)
        {
            logger.LogInformation("User inactive. DePublish reviews.");

            var userId = Guid.Parse(userStatusChangedEvent.UserId);
            var reviews = await reviewRepository.GetReviewsByUserIdAsync(userId);
            foreach (var review in reviews)
            {
                review.IsPublished = false;
                await reviewRepository.Update(review);
            }
        }
    }
}
