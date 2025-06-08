using CheflienWebApi.Application.Alergies.DTOs;
using CheflienWebApi.Application.Alergies.Interfaces;

namespace CheflienWebApi.Presentation.Endpoints;

public static class AlergieEndpoints
{
    public static void MapAlergieEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/alergies")
            .WithTags("Alergies")
            .WithOpenApi();

        group.MapGet("/", async (IAlergieService alergieService) =>
        {
            try
            {
                var alergies = await alergieService.GetAllAsync();
                return Results.Ok(alergies);
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    title: "An error occurred while retrieving allergies",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status400BadRequest);
            }
        })
        .WithName("GetAllAlergies")
        .WithSummary("Get all allergies")
        .WithDescription("Retrieves all allergies from the database")
        .Produces<IList<AlergieDto>>()
        .Produces(StatusCodes.Status400BadRequest);
    }
} 