namespace CheflienWebApi.Application.Authorization.Dtos;

public record RegisterDto
{
    public required string Role { get; init; }
    public required string UserName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}