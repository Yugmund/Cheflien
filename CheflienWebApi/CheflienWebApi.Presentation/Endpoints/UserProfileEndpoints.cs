using CheflienWebApi.Application.UserProfileUpdate.DTOs;
using CheflienWebApi.Application.UserProfileUpdate.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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
            if (string.IsNullOrEmpty(userId))
                return Results.Unauthorized();

            try
            {
                var profile = await userProfileService.GetUserProfileAsync(userId);
                return Results.Ok(profile);
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while retrieving the user profile",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status400BadRequest);
            }
        })
        .WithName("GetUserProfile")
        .WithSummary("Get current user profile")
        .WithDescription("Retrieves the profile information for the currently authenticated user")
        .Produces<UserProfileDto>()
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status403Forbidden);

        group.MapPut("/", async (
            [FromBody] UpdateUserProfileDto updateDto,
            IUserProfileService userProfileService,
            ClaimsPrincipal user) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Results.Unauthorized();

            try
            {
                var updatedProfile = await userProfileService.UpdateUserProfileAsync(userId, updateDto);
                return Results.Ok(updatedProfile);
            }
            // catch (KeyNotFoundException)
            // {
            //     return Results.BadRequest(new { Message = "User profile not found" });
            // }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new { ex.Message });
            }
            catch (ValidationException ex)
            {
                return Results.BadRequest(new { ex.Message });
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
        .WithDescription("Updates the profile information for the currently authenticated user")
        .Accepts<UpdateUserProfileDto>("application/json")
        .Produces<UserProfileDto>()
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status403Forbidden);
    }
} 