using System.Security.Claims;
using CheflienWebApi.Application.Recommendations.Interfaces;
using CheflienWebApi.Application.Recipes.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheflienWebApi.Presentation.Endpoints;

public static class RecommendationEndpoints
{
    public static void MapRecommendationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/recommendations")
            .WithTags("Recommendations")
            .RequireAuthorization()
            .WithOpenApi();

        group.MapGet("/recipes", async (
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            IRecommendationService recommendationService,
            ClaimsPrincipal user) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Results.Unauthorized();

            try
            {
                var recipes = await recommendationService.GetRecommendedRecipesAsync(userId, pageNumber, pageSize);
                return Results.Ok(recipes);
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while getting recipe recommendations",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status400BadRequest);
            }
        })
        .WithName("GetRecommendedRecipes")
        .WithSummary("Get recommended recipes based on user profile")
        .Produces<PaginatedResultDto<RecipeDto>>()
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized);
    }
} 