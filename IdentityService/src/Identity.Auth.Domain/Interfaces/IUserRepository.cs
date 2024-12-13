using Identity.AuthService.Domain.Entities;

namespace Identity.AuthService.Domain.Interfaces;

public interface IUserRepository : IRepositoryBase<User, string>
{
    Task<User?> GetByEmailAsync(string email);
}