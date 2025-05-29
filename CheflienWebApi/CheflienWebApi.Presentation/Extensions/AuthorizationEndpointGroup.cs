using BookingWebApi.Extensions;
using CheflienWebApi.Application.Authorization.Dtos;
using CheflienWebApi.Application.Authorization.Services;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace CheflienWebApi.Presentation.Extensions;

public static class AuthorizationEndpointGroup
{
    public static void MapAuthorizationEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var endpointGroup = endpoints
            .MapGroup("/api")
            .AddFluentValidationAutoValidation()
            .WithTags("Authorization");
        
        endpointGroup.MapPost("/register", 
            async (RegisterDto registerDto, IAuthorizationService authorizationService) =>
            {
                var result = await authorizationService.RegisterUser(registerDto);
                return result.ToResponse();
            })
            .Produces<string>()
            .Produces(StatusCodes.Status400BadRequest);

        endpointGroup.MapPost("/login", 
            async (LoginDto loginDto, IAuthorizationService authorizationService) =>
            {
                var result = await authorizationService.LoginUser(loginDto);
                return result.ToResponse();
            })
            .Produces<string>()
            .Produces(StatusCodes.Status400BadRequest);
    }
}