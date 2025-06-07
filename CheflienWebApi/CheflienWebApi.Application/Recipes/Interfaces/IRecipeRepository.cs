using CheflienWebApi.Application.Recipes.DTOs;

namespace CheflienWebApi.Application.Recipes.Interfaces;

public interface IRecipeRepository
{
    Task<(IList<Domain.Entities.Recipe> Items, int TotalCount)> GetFilteredAsync(
        RecipeFilterDto filter,
        int pageNumber,
        int pageSize);
} 