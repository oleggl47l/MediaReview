using MediaReview.Identity.Domain.Entities;

namespace MediaReview.Identity.Domain.Interfaces;

public interface IJwtGenerator
{
    string GenerateToken(User user, IList<string> roles);
    (string RefreshToken, DateTime Expires) GenerateRefreshToken();
}