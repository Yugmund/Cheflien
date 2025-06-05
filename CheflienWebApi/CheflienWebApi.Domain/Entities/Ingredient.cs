namespace CheflienWebApi.Domain.Entities;

public class Ingredient
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IList<IngredientInRecipe> IngredientInRecipes { get; set; }
    public IList<Alergie> Alergies { get; set; }
}