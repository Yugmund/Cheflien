using CheflienWebApi.Application.Recipes.DTOs;
using CheflienWebApi.Application.Recipes.Interfaces;
using CheflienWebApi.Domain.Entities;
using CheflienWebApi.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CheflienWebApi.Infrastructure.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly ApplicationDbContext _context;

    public RecipeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Recipe?> GetByIdAsync(Guid id)
    {
        return await _context.Recipes
            .Include(r => r.Ingredients)
                .ThenInclude(i => i.Ingredient)
                    .ThenInclude(i => i.Alergies)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<(IList<Recipe> Items, int TotalCount)> GetFilteredAsync(
        RecipeFilterDto filter,
        int pageNumber,
        int pageSize)
    {
        var query = _context.Recipes
            .Include(r => r.Ingredients)
                .ThenInclude(i => i.Ingredient)
                    .ThenInclude(i => i.Alergies)
            .AsQueryable();

        // Apply filters
        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
        {
            query = query.Where(r => 
                r.Name.Contains(filter.SearchTerm) || 
                r.Description.Contains(filter.SearchTerm));
        }

        if (filter.IngredientIds != null && filter.IngredientIds.Any())
        {
            query = query.Where(r => 
                r.Ingredients.Any(i => filter.IngredientIds.Contains(i.IngredientId)));
        }

        // if (filter.MinCookingTime.HasValue)
        // {
        //     query = query.Where(r => r.CookingTime >= filter.MinCookingTime.Value);
        // }
        //
        // if (filter.MaxCookingTime.HasValue)
        // {
        //     query = query.Where(r => r.CookingTime <= filter.MaxCookingTime.Value);
        // }

        if (filter.MinServings.HasValue)
        {
            query = query.Where(r => r.Servings >= filter.MinServings.Value);
        }

        if (filter.MaxServings.HasValue)
        {
            query = query.Where(r => r.Servings <= filter.MaxServings.Value);
        }

        // Get total count
        var totalCount = await query.CountAsync();

        // Apply pagination
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
} 