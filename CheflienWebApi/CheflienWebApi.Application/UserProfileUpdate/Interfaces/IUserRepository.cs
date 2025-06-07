using CheflienWebApi.Domain.Entities;

namespace CheflienWebApi.Application.UserProfileUpdate.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(string id);
    Task<User?> GetByIdWithAlergiesAsync(string id);
    Task UpdateAsync(User user);
    Task<IList<Alergie>> GetUserAlergiesAsync(string userId);
    Task UpdateUserAlergiesAsync(string userId, IList<Guid> alergieIds);
} 