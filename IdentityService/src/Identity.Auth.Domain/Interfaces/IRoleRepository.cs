using Identity.AuthService.Domain.Entities;

namespace Identity.AuthService.Domain.Interfaces;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetActiveRolesAsync();
}