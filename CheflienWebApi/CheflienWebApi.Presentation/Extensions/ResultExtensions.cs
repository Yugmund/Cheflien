using FluentResults;

namespace BookingWebApi.Extensions;

public static class ResultExtensions
{
    public static IResult ToResponse<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }

        return Results.BadRequest(result.Errors);
    }
}