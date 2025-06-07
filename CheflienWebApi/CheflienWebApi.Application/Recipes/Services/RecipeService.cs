using CheflienWebApi.Application.Recipes.DTOs;
using CheflienWebApi.Application.Recipes.Interfaces;

namespace CheflienWebApi.Application.Recipes.Services;

public class RecipeService(IRecipeRepository recipeRepository) : IRecipeService
{
    public async Task<PaginatedResultDto<RecipeDto>> GetFilteredAsync(
        RecipeFilterDto filter,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var (items, totalCount) = await recipeRepository.GetFilteredAsync(filter, pageNumber, pageSize);
        
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
} 