using Microsoft.AspNetCore.Identity;

namespace BookingWebApi.Application.Authorization.Wrappers;

public interface IRoleManagerWrapper
{
    Task<bool> RoleExistsAsync(string registerDtoRole);
    Task<IdentityRole?> FindByNameAsync(string registerDtoRole);
}

public class RoleManagerWrapper(RoleManager<IdentityRole> roleManager) : IRoleManagerWrapper
{
    public async Task<bool> RoleExistsAsync(string registerDtoRole)
    {
        return await roleManager.RoleExistsAsync(registerDtoRole);
    }

    public async Task<IdentityRole?> FindByNameAsync(string registerDtoRole)
    {
        return await roleManager.FindByNameAsync(registerDtoRole);
    }
}