using Identity.AuthService.Domain.Entities;
using Identity.AuthService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Identity.Auth.Infrastructure.Data.Repositories;

public class RoleRepository(ApplicationDbContext context) : RepositoryBase<Role, string>(context), IRoleRepository
{
    public async Task<IEnumerable<Role>> GetActiveRolesAsync()
    {
        return await DbSet.Where(r => r.IsActive).ToListAsync();
    }
}