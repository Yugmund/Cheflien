using CheflienWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheflienWebApi.Infrastructure.EntityConfigurations;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Steps)
            .IsRequired();

        builder.Property(r => r.CookingTime)
            .IsRequired();

        builder.Property(r => r.Servings)
            .IsRequired();

        builder.Property(r => r.Description)
            .HasMaxLength(1000);

        // Configure one-to-many relationship with IngredientInRecipe
        builder.HasMany(r => r.Ingredients)
            .WithOne(iir => iir.Recipe)
            .HasForeignKey(iir => iir.RecipeId)
            .IsRequired();

        // Configure many-to-many relationship with User
        builder.HasMany(r => r.Users)
            .WithMany(u => u.Recipes)
            .UsingEntity(j => j.ToTable("UserRecipes"));
    }
} 