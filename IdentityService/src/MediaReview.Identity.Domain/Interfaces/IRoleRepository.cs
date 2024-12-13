using MediaReview.Identity.Domain.Entities;

namespace MediaReview.Identity.Domain.Interfaces;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetActiveRolesAsync();
}