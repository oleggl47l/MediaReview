using MediaReview.Identity.Domain.Entities;

namespace MediaReview.Identity.Domain.Interfaces;

public interface IUserRepository : IRepositoryBase<User, string>
{
    Task<User?> GetByEmailAsync(string email);
}