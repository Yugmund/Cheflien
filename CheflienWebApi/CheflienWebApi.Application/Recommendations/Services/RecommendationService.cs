using CheflienWebApi.Application.Recommendations.Interfaces;
using CheflienWebApi.Application.Recipes.DTOs;
using CheflienWebApi.Application.UserProfileUpdate.Interfaces;
using CheflienWebApi.Application.Recipes.Interfaces;
using CheflienWebApi.Domain.Entities;
using AlergieDto = CheflienWebApi.Application.Recipes.DTOs.AlergieDto;

namespace CheflienWebApi.Application.Recommendations.Services;

public class RecommendationService : IRecommendationService
{
    private readonly IUserRepository _userRepository;
    private readonly IRecipeRepository _recipeRepository;

    public RecommendationService(
        IUserRepository userRepository,
        IRecipeRepository recipeRepository)
    {
        _userRepository = userRepository;
        _recipeRepository = recipeRepository;
    }

    public async Task<PaginatedResultDto<RecipeDto>> GetRecommendedRecipesAsync(string userId, int pageNumber = 1, int pageSize = 10)
    {
        // Get user's allergies
        var userAlergies = await _userRepository.GetUserAlergiesAsync(userId);
        var userAlergieIds = userAlergies.Select(a => a.Id).ToList();

        // Get all recipes
        var (recipes, totalCount) = await _recipeRepository.GetFilteredAsync(new RecipeFilterDto(), 1, 1000);

        // Filter and score recipes
        var scoredRecipes = recipes
            .Select(recipe => new
            {
                Recipe = recipe,
                Score = CalculateRecipeScore(recipe, userAlergieIds)
            })
            .Where(r => r.Score > 0) // Filter out recipes with incompatible allergies
            .OrderByDescending(r => r.Score)
            .ToList();

        // Apply pagination
        var paginatedRecipes = scoredRecipes
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(r => MapToDto(r.Recipe, userAlergieIds))
            .ToList();

        var totalPages = (int)Math.Ceiling(scoredRecipes.Count / (double)pageSize);

        return new PaginatedResultDto<RecipeDto>
        {
            Items = paginatedRecipes,
            TotalCount = scoredRecipes.Count,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            HasPreviousPage = pageNumber > 1,
            HasNextPage = pageNumber < totalPages
        };
    }

    private static double CalculateRecipeScore(Domain.Entities.Recipe recipe, IList<Guid> userAlergieIds)
    {
        // Get all allergies from recipe ingredients
        var recipeAlergies = recipe.Ingredients
            .SelectMany(i => i.Ingredient.Alergies)
            .Select(a => a.Id)
            .Distinct()
            .ToList();

        // If recipe contains any of user's allergies, return 0 (incompatible)
        if (recipeAlergies.Any(a => userAlergieIds.Contains(a)))
            return 0;

        // Calculate base score (can be enhanced with more factors)
        double score = 100;

        // Reduce score based on number of ingredients (simpler recipes get higher score)
        score -= recipe.Ingredients.Count * 2;

        // Reduce score based on cooking time (faster recipes get higher score)
        if (int.TryParse(recipe.CookingTime.Split(' ')[0], out int cookingTime))
        {
            score -= cookingTime * 0.5;
        }

        return Math.Max(0, score);
    }

    private static RecipeDto MapToDto(Domain.Entities.Recipe recipe, IList<Guid> userAlergieIds)
    {
        return new RecipeDto
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            Steps = recipe.Steps,
            CookingTime = recipe.CookingTime,
            Servings = recipe.Servings,
            Ingredients = recipe.Ingredients.Select(i => new IngredientDto
            {
                Id = i.Ingredient.Id,
                Name = i.Ingredient.Name,
                Description = i.Ingredient.Description
            }).ToList(),
            Alergies = recipe.Ingredients
                .SelectMany(i => i.Ingredient.Alergies)
                .Where(a => userAlergieIds.Contains(a.Id))
                .DistinctBy(a => a.Id)
                .Select(a => new AlergieDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description
                })
                .ToList()
        };
    }
} 