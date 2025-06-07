using System.Security.Claims;
using CheflienWebApi.Application.Recipes.DTOs;
using CheflienWebApi.Application.Recipes.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CheflienWebApi.Presentation.Endpoints;

public static class RecipeEndpoints
{
    public static void MapRecipeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/recipes")
            .WithTags("Recipes")
            .WithOpenApi();

        group.MapGet("/{id:guid}", async (
            Guid id,
            IRecipeService recipeService,
            ClaimsPrincipal user,
            CancellationToken cancellationToken) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var recipe = await recipeService.GetByIdAsync(id, userId);
            return recipe == null ? Results.NotFound() : Results.Ok(recipe);
        })
        .WithName("GetRecipeById")
        .WithSummary("Get recipe by ID")
        .Produces<RecipeResponseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/", async (
            [FromQuery] string? searchTerm,
            [FromQuery] string? ingredientIds,
            [FromQuery] int? minServings,
            [FromQuery] int? maxServings,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            IRecipeService recipeService) =>
        {
            try
            {
                var filter = new RecipeFilterDto
                {
                    SearchTerm = searchTerm,
                    IngredientIds = !string.IsNullOrEmpty(ingredientIds) 
                        ? ingredientIds.Split(',').Select(Guid.Parse).ToList() 
                        : null,
                    MinServings = minServings,
                    MaxServings = maxServings
                };

                var recipes = await recipeService.GetFilteredAsync(filter, pageNumber, pageSize);
                return Results.Ok(recipes);
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while retrieving recipes",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status400BadRequest);
            }
        })
        .WithName("GetRecipes")
        .WithSummary("Get filtered and paginated recipes")
        .WithDescription("Retrieves recipes with optional filtering and pagination")
        .Produces<PaginatedResultDto<RecipeDto>>()
        .Produces(StatusCodes.Status400BadRequest);
    }
} 