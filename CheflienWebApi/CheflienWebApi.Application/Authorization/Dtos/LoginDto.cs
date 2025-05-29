namespace CheflienWebApi.Application.Authorization.Dtos;

public record LoginDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}