using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "ImagePath", "IsFavorite", "Name" },
                values: new object[,]
                {
                    { new Guid("2fd73246-ad73-414f-9f29-c7820ec9c3a9"), 5000m, "Исключительно натуральные материалы", "/images/image3.jpg", false, "Reebok для дома" },
                    { new Guid("399376e5-1ada-486f-acbf-033e6d6ba649"), 25000m, "Исключительно натуральные материалы", "/images/image2.jpg", false, "Кроссовки Nike" },
                    { new Guid("bc89c001-73af-4e53-9851-bb52be23d487"), 75000m, "Исключительно натуральные материалы", "/images/image6.jpg", false, "Guccci для дома" },
                    { new Guid("c49c6475-b8c9-4cbe-92cb-c9424b07453d"), 15000m, "Исключительно натуральные материалы", "/images/image1.jpg", false, "Туфли Lacoste" },
                    { new Guid("cd71401a-3492-451f-bceb-172e89327423"), 12000m, "Исключительно натуральные материалы", "/images/image5.jpg", false, "Кроссовки Adidas" },
                    { new Guid("dff9eb75-338d-4bec-acca-536af43f314b"), 45000m, "Исключительно натуральные материалы", "/images/image4.jpg", false, "Туфли Jimmy Choo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2fd73246-ad73-414f-9f29-c7820ec9c3a9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("399376e5-1ada-486f-acbf-033e6d6ba649"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bc89c001-73af-4e53-9851-bb52be23d487"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c49c6475-b8c9-4cbe-92cb-c9424b07453d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cd71401a-3492-451f-bceb-172e89327423"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dff9eb75-338d-4bec-acca-536af43f314b"));
        }
    }
}
