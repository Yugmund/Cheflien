namespace CheflienWebApi.Application.Recipes.DTOs;

public class RecipeResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Steps { get; set; } = string.Empty;
    public string CookingTime { get; set; } = string.Empty;
    public int Servings { get; set; }
    public IList<IngredientInRecipeDto> Ingredients { get; set; } = new List<IngredientInRecipeDto>();
}

public class IngredientInRecipeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Measuring { get; set; } = string.Empty;
    public IList<AlergieDto> Alergies { get; set; } = new List<AlergieDto>();
}