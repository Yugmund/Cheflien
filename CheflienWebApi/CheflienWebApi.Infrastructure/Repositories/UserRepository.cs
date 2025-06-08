using CheflienWebApi.Application.UserProfileUpdate.Interfaces;
using CheflienWebApi.Domain.Entities;
using CheflienWebApi.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CheflienWebApi.Infrastructure.Repositories;

public class UserRepository(UserManager<User> userManager, ApplicationDbContext context)
    : IUserRepository
{
    public async Task<User?> GetByIdAsync(string id)
    {
        return await userManager.FindByIdAsync(id);
    }

    public async Task<User?> GetByIdWithAlergiesAsync(string id)
    {
        return await context.Users
            .Include(u => u.Alergies)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task UpdateAsync(User user)
    {
        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
            throw new InvalidOperationException($"Failed to update user: " +
                $"{string.Join(", ", result.Errors.Select(e => e.Description))}");
    }

    public async Task<IList<Alergie>> GetUserAlergiesAsync(string userId)
    {
        return await context.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Alergies)
            .ToListAsync();
    }

    public async Task UpdateUserAlergiesAsync(string userId, IList<Guid> alergieIds)
    {
        var user = await GetByIdWithAlergiesAsync(userId);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {userId} not found.");

        var currentAlergies = user.Alergies.ToList();
        var alergiesToAdd = await context.Alergies
            .Where(a => alergieIds.Contains(a.Id))
            .ToListAsync();

        user.Alergies.Clear();
        foreach (var alergie in alergiesToAdd)
        {
            user.Alergies.Add(alergie);
        }

        await context.SaveChangesAsync();
    }

    public async Task LikeRecipeAsync(string userId, Guid recipeId)
    {
        var user = await context.Users
            .Include(u => u.Recipes)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            throw new KeyNotFoundException($"User with ID {userId} not found.");

        var recipe = await context.Recipes.FindAsync(recipeId);
        if (recipe == null)
            throw new KeyNotFoundException($"Recipe with ID {recipeId} not found.");

        if (!user.Recipes.Contains(recipe))
        {
            user.Recipes.Add(recipe);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IList<Recipe>> GetFavoriteRecipesAsync(string userId)
    {
        return await context.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Recipes)
            .Include(r => r.Ingredients)
                .ThenInclude(i => i.Ingredient)
                    .ThenInclude(i => i.Alergies)
            .ToListAsync();
    }
} 