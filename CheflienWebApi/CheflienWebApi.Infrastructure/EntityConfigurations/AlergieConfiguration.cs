using CheflienWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheflienWebApi.Infrastructure.EntityConfigurations;

public class AlergieConfiguration : IEntityTypeConfiguration<Alergie>
{
    public void Configure(EntityTypeBuilder<Alergie> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Description)
            .HasMaxLength(500);

        // Configure many-to-many relationship with User
        builder.HasMany(a => a.Users)
            .WithMany(u => u.Alergies)
            .UsingEntity(j => j.ToTable("UserAlergies"));

        // Configure many-to-many relationship with Ingredient
        builder.HasMany(a => a.Ingredients)
            .WithMany(i => i.Alergies)
            .UsingEntity(j => j.ToTable("AlergieIngredients"));
    }
} 