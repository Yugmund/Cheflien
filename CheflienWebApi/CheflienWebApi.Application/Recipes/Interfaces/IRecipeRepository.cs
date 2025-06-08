using CheflienWebApi.Application.Recipes.DTOs;
using CheflienWebApi.Domain.Entities;

namespace CheflienWebApi.Application.Recipes.Interfaces;

public interface IRecipeRepository
{
    Task<Domain.Entities.Recipe?> GetByIdAsync(Guid id);
    Task<(IList<Domain.Entities.Recipe> Items, int TotalCount)> GetFilteredAsync(
        RecipeFilterDto filter,
        int pageNumber,
        int pageSize);
} 