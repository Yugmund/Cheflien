namespace CheflienWebApi.Domain.Constants;

public static class RoleConstants
{
    private const string Admin = "Admin";
    private const string Client = "Client";
    
    public static readonly IReadOnlyList<string> Roles = [Admin, Client];
}
