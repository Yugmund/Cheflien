using CheflienWebApi.Application.Recipes.DTOs;
using CheflienWebApi.Application.UserProfileUpdate.DTOs;
using CheflienWebApi.Application.UserProfileUpdate.Services;

namespace CheflienWebApi.Application.UserProfileUpdate.Interfaces;

public interface IUserProfileService
{
    Task<UserProfileDto?> GetByIdAsync(string id);
    Task UpdateAsync(string id, UpdateUserProfileDto updateDto);
    Task LikeRecipeAsync(string userId, Guid recipeId);
    Task<IList<RecipeDto>> GetFavoriteRecipesAsync(string userId);
} 