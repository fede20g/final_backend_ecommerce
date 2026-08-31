using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ECommerce.Domain.Entities;
using ECommerce.Domain.ValueObjects;

namespace ECommerce.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedNever();

        builder.Property(u => u.Email)
               .HasConversion(e => e.Value, v => Email.Create(v))
               .IsRequired()
               .HasMaxLength(250);
        builder.Property(u => u.Name).IsRequired().HasMaxLength(200);
        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.Role).IsRequired().HasMaxLength(20);
        builder.Property(u => u.CreatedAt).IsRequired();

        // Email debe ser único en la tabla
        builder.HasIndex(u => u.Email).IsUnique();

        // GUIDs FIJOS — nunca usar Guid.NewGuid() en HasData()
        builder.HasData(new
        {
            Id           = new Guid("b1c2d3e4-0000-0000-0000-000000000001"),
            Email        = Email.Create("admin@ecommerce.com"),
            Name         = "Administrador",
            PasswordHash = "$2a$11$ESxpO7zBLO34u7SY/U87ouBIpeJLJGcAHjOi3cDG7Oc2TmnnE/rEW",
            Role         = "Admin",
            CreatedAt    = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });
    }
}
