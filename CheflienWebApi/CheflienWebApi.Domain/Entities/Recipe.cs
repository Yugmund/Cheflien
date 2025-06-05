namespace CheflienWebApi.Domain.Entities;

public class Recipe
{
    public Guid Id { get; set; }
    public string Name { get; set; }    
    public string Steps { get; set; }
    public string CookingTime { get; set; }
    public short Servings { get; set; }
    public string Description { get; set; }
    public IList<IngredientInRecipe> Ingredients { get; set; }
    public IList<User> Users { get; set; }
}