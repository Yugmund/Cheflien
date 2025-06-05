using CheflienWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheflienWebApi.Infrastructure.EntityConfigurations;

public class IngredientInRecipeConfiguration : IEntityTypeConfiguration<IngredientInRecipe>
{
    public void Configure(EntityTypeBuilder<IngredientInRecipe> builder)
    {
        builder.HasKey(iir => iir.Id);

        builder.Property(iir => iir.Measuring)
            .IsRequired()
            .HasMaxLength(50);

        // Configure relationship with Recipe
        builder.HasOne(iir => iir.Recipe)
            .WithMany(r => r.Ingredients)
            .HasForeignKey(iir => iir.RecipeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Configure relationship with Ingredient
        builder.HasOne(iir => iir.Ingredient)
            .WithMany(i => i.IngredientInRecipes)
            .HasForeignKey(iir => iir.IngredientId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
} 