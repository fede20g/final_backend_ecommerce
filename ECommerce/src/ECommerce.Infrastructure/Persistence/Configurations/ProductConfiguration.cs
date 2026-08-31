using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ECommerce.Domain.Entities;

namespace ECommerce.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    private static readonly Guid ElectronicaId = new Guid("a1b2c3d4-0000-0000-0000-000000000001");
    private static readonly Guid RopaId        = new Guid("a1b2c3d4-0000-0000-0000-000000000002");
    private static readonly Guid HogarId       = new Guid("a1b2c3d4-0000-0000-0000-000000000003");
    private static readonly Guid DeportesId    = new Guid("a1b2c3d4-0000-0000-0000-000000000004");

    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();  // Guid generado en Domain

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(p => p.Description)
               .HasMaxLength(2000);

        builder.Property(p => p.Price)
               .IsRequired()
               .HasColumnType("decimal(18,2)");  // precisión monetaria

        builder.Property(p => p.Stock)
               .IsRequired()
               .HasDefaultValue(0);

        builder.Property(p => p.CategoryId).IsRequired();
        builder.Property(p => p.CreatedAt).IsRequired();

        // Índice para búsquedas rápidas por nombre
        builder.HasIndex(p => p.Name);

        // Relación N:1 con Category
        builder.HasOne<Category>()
               .WithMany()
               .HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        // GUIDs FIJOS — nunca usar Guid.NewGuid() en HasData()
        builder.HasData(
            new
            {
                Id          = new Guid("c1d2e3f4-0000-0000-0000-000000000001"),
                Name        = "Samsung Galaxy S24 Ultra",
                Description = "Smartphone flagship de Samsung con pantalla Dynamic AMOLED 6.8\", cámara de 200MP, S Pen integrado y batería de 5000mAh. Procesador Snapdragon 8 Gen 3.",
                Price       = 1299.99m,
                Stock       = 50,
                CategoryId  = ElectronicaId,
                CreatedAt   = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id          = new Guid("c1d2e3f4-0000-0000-0000-000000000002"),
                Name        = "Apple iPhone 15 Pro",
                Description = "iPhone con chip A17 Pro, cámara principal de 48MP con zoom óptico 3x, pantalla Super Retina XDR 6.1\" y estructura de titanio. iOS 17.",
                Price       = 1199.99m,
                Stock       = 35,
                CategoryId  = ElectronicaId,
                CreatedAt   = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id          = new Guid("c1d2e3f4-0000-0000-0000-000000000003"),
                Name        = "Campera de Cuero Genuino",
                Description = "Campera de cuero genuino vacuno, corte slim fit, cierre YKK, bolsillos interiores. Disponible en negro y marrón. Tallas S a XXL.",
                Price       = 189.99m,
                Stock       = 80,
                CategoryId  = RopaId,
                CreatedAt   = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id          = new Guid("c1d2e3f4-0000-0000-0000-000000000004"),
                Name        = "Cafetera Espresso De'Longhi Dedica",
                Description = "Cafetera espresso compacta con bomba de 15 bares, vaporizador de leche ajustable, compatible con café molido y cápsulas ESE. Potencia 1300W.",
                Price       = 299.99m,
                Stock       = 25,
                CategoryId  = HogarId,
                CreatedAt   = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id          = new Guid("c1d2e3f4-0000-0000-0000-000000000005"),
                Name        = "Pelota de Fútbol Adidas Tiro League",
                Description = "Pelota oficial de entrenamiento Adidas, superficie termosellada para mayor durabilidad, cámara de butilo para retención de aire. Talla 5. FIFA Basic.",
                Price       = 59.99m,
                Stock       = 200,
                CategoryId  = DeportesId,
                CreatedAt   = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
