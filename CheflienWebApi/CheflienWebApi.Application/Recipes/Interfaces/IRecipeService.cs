using CheflienWebApi.Application.Recipes.DTOs;

namespace CheflienWebApi.Application.Recipes.Interfaces;

public interface IRecipeService
{
    Task<RecipeResponseDto?> GetByIdAsync(Guid id);
    Task<PaginatedResultDto<RecipeDto>> GetFilteredAsync(
        RecipeFilterDto filter,
        int pageNumber = 1,
        int pageSize = 10);
} 