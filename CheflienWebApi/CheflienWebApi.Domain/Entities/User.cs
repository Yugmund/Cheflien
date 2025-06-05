using Microsoft.AspNetCore.Identity;

namespace CheflienWebApi.Domain.Entities;

public class User : IdentityUser
{
    public IList<IdentityUserRole<string>> UserRoles { get; set; }
    public IList<Alergie> Alergies { get; set; }
    public IList<Recipe> Recipes { get; set; }
}