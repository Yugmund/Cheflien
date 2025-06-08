using CheflienWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheflienWebApi.Infrastructure.EntityConfigurations;

public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.Description)
            .HasMaxLength(500);

        // Configure one-to-many relationship with IngredientInRecipe
        builder.HasMany(i => i.IngredientInRecipes)
            .WithOne(iir => iir.Ingredient)
            .HasForeignKey(iir => iir.IngredientId)
            .IsRequired();

        // Configure many-to-many relationship with Alergie
        builder.HasMany(i => i.Alergies)
            .WithMany(a => a.Ingredients)
            .UsingEntity(j => j.ToTable("AlergieIngredients"));
    }
} 