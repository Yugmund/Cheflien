using CheflienWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CheflienWebApi.Application.Authorization.Wrappers;

public interface IUserManagerWrapper
{
    Task<IdentityResult> CreateAsync(User user, string registerDtoPassword);
    Task<User> FindByEmailAsync(string registerDtoEmail);
    Task<bool> CheckPasswordAsync(User user, string loginDtoPassword);
    Task<IList<string>> GetRolesAsync(User user);
}

public class UserManagerWrapper(UserManager<User> userManager) : IUserManagerWrapper
{
    public async Task<IdentityResult> CreateAsync(User user, string registerDtoPassword)
    {
        return await userManager.CreateAsync(user, registerDtoPassword);
    }

    public async Task<User> FindByEmailAsync(string registerDtoEmail)
    {
        return await userManager.FindByEmailAsync(registerDtoEmail);
    }

    public async Task<bool> CheckPasswordAsync(User user, string loginDtoPassword)
    {
        return await userManager.CheckPasswordAsync(user, loginDtoPassword);
    }

    public async Task<IList<string>> GetRolesAsync(User user)
    {
        return await userManager.GetRolesAsync(user);
    }
}