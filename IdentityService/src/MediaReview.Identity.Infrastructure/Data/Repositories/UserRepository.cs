using MediaReview.Identity.Domain.Entities;
using MediaReview.Identity.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediaReview.Identity.Infrastructure.Data.Repositories;

public class UserRepository(DbContext context) : RepositoryBase<User, string>(context), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await DbSet.FirstOrDefaultAsync(u => u.Email == email);
    }
}