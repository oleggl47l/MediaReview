using MediaReview.Identity.Domain.Entities;
using MediaReview.Identity.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediaReview.Identity.Infrastructure.Data.Repositories;

public class RoleRepository(ApplicationDbContext context) : RepositoryBase<Role, string>(context), IRoleRepository
{
    public async Task<IEnumerable<Role>> GetActiveRolesAsync()
    {
        return await DbSet.Where(r => r.IsActive).ToListAsync();
    }
}