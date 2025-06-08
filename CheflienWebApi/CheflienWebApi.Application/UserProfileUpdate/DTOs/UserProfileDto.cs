namespace CheflienWebApi.Application.UserProfileUpdate.DTOs;

public class AlergieDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class UserProfileDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public IList<AlergieDto> Alergies { get; set; }
}

public class UpdateUserProfileDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public IList<Guid> AlergieIds { get; set; }
} 