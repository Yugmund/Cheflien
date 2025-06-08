using CheflienWebApi.Application.UserProfileUpdate.DTOs;
using CheflienWebApi.Application.UserProfileUpdate.Interfaces;
using System.Security.Claims;
using CheflienWebApi.Application.Recipes.DTOs;

namespace CheflienWebApi.Presentation.Endpoints;

/// <summary>
/// Endpoints for managing user profiles
/// </summary>
public static class UserProfileEndpoints
{
    /// <summary>
    /// Maps user profile related endpoints
    /// </summary>
    /// <param name="app">The endpoint route builder</param>
    public static void MapUserProfileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/user-profile")
            .WithTags("User Profile")
            .RequireAuthorization()
            .WithOpenApi();

        group.MapGet("/", async (
            IUserProfileService userProfileService,
            ClaimsPrincipal user) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Results.Unauthorized();

            var profile = await userProfileService.GetByIdAsync(userId);
            return profile == null ? Results.NotFound() : Results.Ok(profile);
        })
        .WithName("GetUserProfile")
        .WithSummary("Get current user profile")
        .Produces<UserProfileDto>()
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/", async (
            UpdateUserProfileDto dto,
            IUserProfileService userProfileService,
            ClaimsPrincipal user) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Results.Unauthorized();

            try
            {
                await userProfileService.UpdateAsync(userId, dto);
                return Results.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while updating the user profile",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status400BadRequest);
            }
        })
        .WithName("UpdateUserProfile")
        .WithSummary("Update current user profile")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/recipes/{recipeId:guid}/like", async (
            Guid recipeId,
            IUserProfileService userProfileService,
            ClaimsPrincipal user) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Results.Unauthorized();

            try
            {
                await userProfileService.LikeRecipeAsync(userId, recipeId);
                return Results.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while liking the recipe",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status400BadRequest);
            }
        })
        .WithName("LikeRecipe")
        .WithSummary("Like a recipe")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/recipes/favorites", async (
            IUserProfileService userProfileService,
            ClaimsPrincipal user) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Results.Unauthorized();

            try
            {
                var recipes = await userProfileService.GetFavoriteRecipesAsync(userId);
                return Results.Ok(recipes);
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while retrieving favorite recipes",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status400BadRequest);
            }
        })
        .WithName("GetFavoriteRecipes")
        .WithSummary("Get user's favorite recipes")
        .Produces<IList<RecipeDto>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .RequireAuthorization();
    }
} 