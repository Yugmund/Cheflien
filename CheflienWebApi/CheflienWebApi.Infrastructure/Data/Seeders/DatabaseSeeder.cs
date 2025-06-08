using CheflienWebApi.Domain.Entities;
using CheflienWebApi.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CheflienWebApi.Infrastructure.Data.Seeders;

public static class DatabaseSeeder
{
    public static async Task SeedDataAsync(ApplicationDbContext context)
    {
        if (!await context.Alergies.AnyAsync())
        {
            await SeedAlergiesAsync(context);
        }

        if (!await context.Ingredients.AnyAsync())
        {
            await SeedIngredientsAsync(context);
        }

        if (!await context.Recipes.AnyAsync())
        {
            await SeedRecipesAsync(context);
        }
    }

    private static async Task SeedAlergiesAsync(ApplicationDbContext context)
    {
        var allergies = new List<Alergie>
        {
            new() { Name = "Gluten", Description = "Gluten intolerance" },
            new() { Name = "Lactose", Description = "Lactose intolerance" },
            new() { Name = "Nuts", Description = "Nut allergy" },
            new() { Name = "Shellfish", Description = "Shellfish allergy" },
            new() { Name = "Eggs", Description = "Egg allergy" },
            new() { Name = "Soy", Description = "Soy allergy" },
            new() { Name = "Fish", Description = "Fish allergy" },
            new() { Name = "Peanuts", Description = "Peanut allergy" }
        };

        await context.Alergies.AddRangeAsync(allergies);
        await context.SaveChangesAsync();
    }

    private static async Task SeedIngredientsAsync(ApplicationDbContext context)
    {
        var ingredients = new List<Ingredient>
        {
            new() { Name = "Flour", Description = "All-purpose flour" },
            new() { Name = "Sugar", Description = "White granulated sugar" },
            new() { Name = "Salt", Description = "Table salt" },
            new() { Name = "Butter", Description = "Unsalted butter" },
            new() { Name = "Eggs", Description = "Chicken eggs" },
            new() { Name = "Milk", Description = "Whole milk" },
            new() { Name = "Olive Oil", Description = "Extra virgin olive oil" },
            new() { Name = "Garlic", Description = "Fresh garlic" },
            new() { Name = "Onion", Description = "Yellow onion" },
            new() { Name = "Tomatoes", Description = "Fresh tomatoes" },
            new() { Name = "Chicken", Description = "Chicken breast" },
            new() { Name = "Rice", Description = "Long grain white rice" },
            new() { Name = "Pasta", Description = "Spaghetti" },
            new() { Name = "Cheese", Description = "Cheddar cheese" },
            new() { Name = "Bread", Description = "White bread" }
        };

        await context.Ingredients.AddRangeAsync(ingredients);
        await context.SaveChangesAsync();

        // Add ingredient-allergy connections
        var ingredientAllergies = new Dictionary<string, string[]>
        {
            { "Flour", new[] { "Gluten" } },
            { "Bread", new[] { "Gluten" } },
            { "Pasta", new[] { "Gluten" } },
            { "Milk", new[] { "Lactose" } },
            { "Cheese", new[] { "Lactose" } },
            { "Butter", new[] { "Lactose" } },
            { "Eggs", new[] { "Eggs" } },
            { "Chicken", new[] { "Eggs" } } // Some chicken products might contain eggs
        };

        foreach (var (ingredientName, allergyNames) in ingredientAllergies)
        {
            var ingredient = await context.Ingredients.FirstAsync(i => i.Name == ingredientName);
            var allergies = await context.Alergies
                .Where(a => allergyNames.Contains(a.Name))
                .ToListAsync();

            ingredient.Alergies = allergies;
        }

        await context.SaveChangesAsync();
    }

    private static async Task SeedRecipesAsync(ApplicationDbContext context)
    {
        var recipes = new List<Recipe>
        {
            new()
            {
                Name = "Classic Spaghetti Carbonara",
                Description = "Traditional Italian pasta dish with eggs, cheese, pancetta, and black pepper",
                Steps = "1. Cook pasta\n2. Fry pancetta\n3. Mix eggs and cheese\n4. Combine all ingredients",
                CookingTime = "30 minutes",
                Servings = 4,
                Ingredients = new List<IngredientInRecipe>
                {
                    new() { IngredientId = (await context.Ingredients.FirstAsync(i => i.Name == "Pasta")).Id, Measuring = "200g" },
                    new() { IngredientId = (await context.Ingredients.FirstAsync(i => i.Name == "Eggs")).Id, Measuring = "2 pieces" },
                    new() { IngredientId = (await context.Ingredients.FirstAsync(i => i.Name == "Cheese")).Id, Measuring = "100g" },
                    new() { IngredientId = (await context.Ingredients.FirstAsync(i => i.Name == "Salt")).Id, Measuring = "1 tsp" }
                }
            },
            new()
            {
                Name = "Simple Chicken Stir-Fry",
                Description = "Quick and healthy chicken stir-fry with vegetables",
                Steps = "1. Cut chicken\n2. Prepare vegetables\n3. Stir-fry chicken\n4. Add vegetables\n5. Season",
                CookingTime = "25 minutes",
                Servings = 2,
                Ingredients = new List<IngredientInRecipe>
                {
                    new() { IngredientId = (await context.Ingredients.FirstAsync(i => i.Name == "Chicken")).Id, Measuring = "300g" },
                    new() { IngredientId = (await context.Ingredients.FirstAsync(i => i.Name == "Olive Oil")).Id, Measuring = "2 tbsp" },
                    new() { IngredientId = (await context.Ingredients.FirstAsync(i => i.Name == "Garlic")).Id, Measuring = "2 cloves" },
                    new() { IngredientId = (await context.Ingredients.FirstAsync(i => i.Name == "Onion")).Id, Measuring = "1 piece" },
                    new() { IngredientId = (await context.Ingredients.FirstAsync(i => i.Name == "Tomatoes")).Id, Measuring = "2 pieces" }
                }
            },
            new()
            {
                Name = "Basic Rice Pilaf",
                Description = "Simple and flavorful rice dish",
                Steps = "1. Rinse rice\n2. Sauté onions\n3. Add rice and water\n4. Cook until done",
                CookingTime = "20 minutes",
                Servings = 4,
                Ingredients = new List<IngredientInRecipe>
                {
                    new() { IngredientId = (await context.Ingredients.FirstAsync(i => i.Name == "Rice")).Id, Measuring = "200g" },
                    new() { IngredientId = (await context.Ingredients.FirstAsync(i => i.Name == "Olive Oil")).Id, Measuring = "1 tbsp" },
                    new() { IngredientId = (await context.Ingredients.FirstAsync(i => i.Name == "Onion")).Id, Measuring = "1 piece" },
                    new() { IngredientId = (await context.Ingredients.FirstAsync(i => i.Name == "Salt")).Id, Measuring = "1 tsp" }
                }
            }
        };

        await context.Recipes.AddRangeAsync(recipes);
        await context.SaveChangesAsync();
    }
} 