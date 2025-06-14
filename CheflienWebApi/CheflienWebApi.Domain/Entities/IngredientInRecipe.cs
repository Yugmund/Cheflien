namespace CheflienWebApi.Domain.Entities;

public class IngredientInRecipe
{
    public Guid Id { get; set; }
    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    public Guid IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }
    public string Measuring { get; set; }
}