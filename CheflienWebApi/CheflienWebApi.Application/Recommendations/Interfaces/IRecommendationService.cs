using CheflienWebApi.Application.Recipes.DTOs;

namespace CheflienWebApi.Application.Recommendations.Interfaces;

public interface IRecommendationService
{
    Task<PaginatedResultDto<RecipeDto>> GetRecommendedRecipesAsync(string userId, int pageNumber = 1, int pageSize = 10);
} 