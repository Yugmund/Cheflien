using CheflienWebApi.Application.Recipes.DTOs;
using CheflienWebApi.Application.Recipes.Interfaces;
using CheflienWebApi.Application.UserProfileUpdate.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CheflienWebApi.Application.Recipes.Services;

public class RecipeService(
    IRecipeRepository recipeRepository,
    IUserRepository userRepository)
    : IRecipeService
{
    public async Task<RecipeResponseDto?> GetByIdAsync(Guid id, string? userId)
    {
        var recipe = await recipeRepository.GetByIdAsync(id);
        if (recipe == null)
            return null;

        var userAlergies = await GetUserAlergiesAsync(userId);
        return MapToDto(recipe, userAlergies);
    }

    public async Task<PaginatedResultDto<RecipeDto>> GetFilteredAsync(
        RecipeFilterDto filter,
        int pageNumber = 1,
        int pageSize = 10,
        string? userId = null)
    {
        var (items, totalCount) = await recipeRepository.GetFilteredAsync(filter, pageNumber, pageSize);
        var userAlergies = await GetUserAlergiesAsync(userId);
        
        var recipes = items.Select(r => new RecipeDto
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
                .DistinctBy(a => a.Id)
                .Select(a => new AlergieDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description
                })
                .ToList()
        }).ToList();

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PaginatedResultDto<RecipeDto>
        {
            Items = recipes,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            HasPreviousPage = pageNumber > 1,
            HasNextPage = pageNumber < totalPages
        };
    }

    private static RecipeResponseDto MapToDto(Domain.Entities.Recipe recipe, IList<Guid>? userAlergies)
    {
        return new RecipeResponseDto
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            Steps = recipe.Steps,
            CookingTime = recipe.CookingTime,
            Servings = recipe.Servings,
            Ingredients = recipe.Ingredients.Select(i => new IngredientInRecipeDto
            {
                Id = i.Ingredient.Id,
                Name = i.Ingredient.Name,
                Measuring = i.Measuring,
                Alergies = userAlergies == null 
                    ? i.Ingredient.Alergies.Select(a => new AlergieDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Description = a.Description
                    }).ToList()
                    : i.Ingredient.Alergies
                        .Where(a => userAlergies.Contains(a.Id))
                        .Select(a => new AlergieDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Description = a.Description
                        }).ToList()
            }).ToList()
        };
    }

    private async Task<IList<Guid>?> GetUserAlergiesAsync(string? userId)
    {
        if (userId == null)
            return null;
        var userAlergies = await userRepository.GetUserAlergiesAsync(userId);
        return userAlergies.Select(a => a.Id).ToList();
    }
} 