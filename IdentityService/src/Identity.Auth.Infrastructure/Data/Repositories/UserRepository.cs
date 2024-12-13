using Identity.AuthService.Domain.Entities;
using Identity.AuthService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Identity.Auth.Infrastructure.Data.Repositories;

public class UserRepository(DbContext context) : RepositoryBase<User, string>(context), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await DbSet.FirstOrDefaultAsync(u => u.Email == email);
    }
}