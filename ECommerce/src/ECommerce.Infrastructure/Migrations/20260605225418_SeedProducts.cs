using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { new Guid("c1d2e3f4-0000-0000-0000-000000000001"), new Guid("a1b2c3d4-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Smartphone flagship de Samsung con pantalla Dynamic AMOLED 6.8\", cámara de 200MP, S Pen integrado y batería de 5000mAh. Procesador Snapdragon 8 Gen 3.", "Samsung Galaxy S24 Ultra", 1299.99m, 50 },
                    { new Guid("c1d2e3f4-0000-0000-0000-000000000002"), new Guid("a1b2c3d4-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "iPhone con chip A17 Pro, cámara principal de 48MP con zoom óptico 3x, pantalla Super Retina XDR 6.1\" y estructura de titanio. iOS 17.", "Apple iPhone 15 Pro", 1199.99m, 35 },
                    { new Guid("c1d2e3f4-0000-0000-0000-000000000003"), new Guid("a1b2c3d4-0000-0000-0000-000000000002"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Campera de cuero genuino vacuno, corte slim fit, cierre YKK, bolsillos interiores. Disponible en negro y marrón. Tallas S a XXL.", "Campera de Cuero Genuino", 189.99m, 80 },
                    { new Guid("c1d2e3f4-0000-0000-0000-000000000004"), new Guid("a1b2c3d4-0000-0000-0000-000000000003"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cafetera espresso compacta con bomba de 15 bares, vaporizador de leche ajustable, compatible con café molido y cápsulas ESE. Potencia 1300W.", "Cafetera Espresso De'Longhi Dedica", 299.99m, 25 },
                    { new Guid("c1d2e3f4-0000-0000-0000-000000000005"), new Guid("a1b2c3d4-0000-0000-0000-000000000004"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pelota oficial de entrenamiento Adidas, superficie termosellada para mayor durabilidad, cámara de butilo para retención de aire. Talla 5. FIFA Basic.", "Pelota de Fútbol Adidas Tiro League", 59.99m, 200 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c1d2e3f4-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c1d2e3f4-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c1d2e3f4-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c1d2e3f4-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c1d2e3f4-0000-0000-0000-000000000005"));
        }
    }
}
