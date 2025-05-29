using Microsoft.AspNetCore.Identity;

namespace CheflienWebApi.Domain.Entities;

public class User : IdentityUser
{
    public string? ExternalId { get; set; }
    public string? SystemId { get; set; }
    public IList<IdentityUserRole<string>> UserRoles { get; set; }
}