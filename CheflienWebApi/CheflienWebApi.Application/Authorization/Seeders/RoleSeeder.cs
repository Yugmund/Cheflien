using CheflienWebApi.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace CheflienWebApi.Application.Authorization.Seeders;

public class RoleSeeder(RoleManager<IdentityRole> roleManager)
{
    public async Task SeedRolesAsync()
    {
        var existingRoles = roleManager.Roles.ToList();
        var existingRoleNames = new HashSet<string>(existingRoles.Select(r => r.Name));
        
        var roleNames = RoleConstants.Roles;

        foreach (var roleName in roleNames.Where(roleName => !existingRoleNames.Contains(roleName)))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}