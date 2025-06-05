namespace CheflienWebApi.Domain.Entities;

public class Alergie
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IList<User> Users { get; set; }
    public IList<Ingredient> Ingredients { get; set; }
}