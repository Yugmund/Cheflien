using CheflienWebApi.Domain.Entities;
using CheflienWebApi.Infrastructure.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CheflienWebApi.Infrastructure.DbContexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Alergie> Alergies { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<IngredientInRecipe> IngredientInRecipes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(InfrastructureAssembly).Assembly);
        
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new RecipeConfiguration());
        builder.ApplyConfiguration(new AlergieConfiguration());
        builder.ApplyConfiguration(new IngredientConfiguration());
        builder.ApplyConfiguration(new IngredientInRecipeConfiguration());
        
        builder.Entity<User>()
            .HasMany(u => u.UserRoles)
            .WithOne()
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
    }
}
