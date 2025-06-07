using CheflienWebApi.Application.Alergies.DTOs;

namespace CheflienWebApi.Application.Recipes.DTOs;

public class RecipeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Steps { get; set; }
    public string CookingTime { get; set; }
    public int Servings { get; set; }
    public IList<IngredientDto> Ingredients { get; set; }
    public IList<AlergieDto> Alergies { get; set; }
}

public class IngredientDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class RecipeFilterDto
{
    public string? SearchTerm { get; set; }
    public IList<Guid>? IngredientIds { get; set; }
    public int? MinCookingTime { get; set; }
    public int? MaxCookingTime { get; set; }
    public int? MinServings { get; set; }
    public int? MaxServings { get; set; }
}

public class PaginatedResultDto<T>
{
    public IList<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
} 