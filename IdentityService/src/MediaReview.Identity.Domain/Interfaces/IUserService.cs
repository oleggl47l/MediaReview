namespace MediaReview.Identity.Domain.Interfaces;

public interface IUserService
{
    Task NotifyUserStatusChanged(string userId);
    Task NotifyUserDeleted(string userId);
}