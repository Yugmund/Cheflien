using CheflienWebApi.Application.Recipes.DTOs;
using CheflienWebApi.Application.UserProfileUpdate.DTOs;
using CheflienWebApi.Application.UserProfileUpdate.Interfaces;
using CheflienWebApi.Domain.Entities;
using AlergieDto = CheflienWebApi.Application.Recipes.DTOs.AlergieDto;

namespace CheflienWebApi.Application.UserProfileUpdate.Services;

public class UserProfileService(IUserRepository userRepository) : IUserProfileService
{
    public async Task<UserProfileDto?> GetByIdAsync(string id)
    {
        var user = await userRepository.GetByIdWithAlergiesAsync(id);
        if (user == null)
            return null;

        return new UserProfileDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Alergies = user.Alergies.Select(a => new DTOs.AlergieDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description
            }).ToList()
        };
    }

    public async Task UpdateAsync(string id, UpdateUserProfileDto dto)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {id} not found.");

        user.UserName = dto.UserName;
        user.Email = dto.Email;
        user.PhoneNumber = dto.PhoneNumber;

        await userRepository.UpdateAsync(user);
        await userRepository.UpdateUserAlergiesAsync(id, dto.AlergieIds);
    }

    public async Task LikeRecipeAsync(string userId, Guid recipeId)
    {
        await userRepository.LikeRecipeAsync(userId, recipeId);
    }

    public async Task<IList<RecipeDto>> GetFavoriteRecipesAsync(string userId)
    {
        var recipes = await userRepository.GetFavoriteRecipesAsync(userId);
        var userAlergies = await userRepository.GetUserAlergiesAsync(userId);
        var userAlergieIds = userAlergies.Select(a => a.Id).ToList();

        return recipes.Select(r => new RecipeDto
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
            Steps = r.Steps,
            CookingTime = r.CookingTime,
            Servings = r.Servings,
            Ingredients = r.Ingredients.Select(i => new IngredientDto
            {
                Id = i.Ingredient.Id,
                Name = i.Ingredient.Name,
                Description = i.Ingredient.Description
            }).ToList(),
            Alergies = r.Ingredients
                .SelectMany(i => i.Ingredient.Alergies)
                .Where(a => userAlergieIds.Contains(a.Id))
                .DistinctBy(a => a.Id)
                .Select<Alergie, AlergieDto>(a => new AlergieDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description
                })
                .ToList<AlergieDto>()
        }).ToList();
    }
}