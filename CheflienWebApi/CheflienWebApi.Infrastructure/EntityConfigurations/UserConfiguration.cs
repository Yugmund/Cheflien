using CheflienWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheflienWebApi.Infrastructure.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("AspNetUsers");
        
        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("IX_AspNetUsers_Email");

        builder.HasIndex(u => u.UserName)
            .IsUnique()
            .HasDatabaseName("IX_AspNetUsers_UserName");

        // Configure many-to-many relationship with Recipe
        builder.HasMany(u => u.Recipes)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.ToTable("UserRecipes"));

        // Configure many-to-many relationship with Alergie
        builder.HasMany(u => u.Alergies)
            .WithMany(a => a.Users)
            .UsingEntity(j => j.ToTable("UserAlergies"));
    }
    
}