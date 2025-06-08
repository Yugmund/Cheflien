using CheflienWebApi.Application.Recipes.DTOs;
using AlergieDto = CheflienWebApi.Application.UserProfileUpdate.DTOs.AlergieDto;

namespace CheflienWebApi.Application.UserProfileUpdate.Services;

public class RecipeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Steps { get; set; }
    public string Description { get; set; }
    public List<AlergieDto> Alergies { get; set; }
    public string CookingTime { get; set; }
    public short Servings { get; set; }
    public List<IngredientDto> Ingredients { get; set; }
}