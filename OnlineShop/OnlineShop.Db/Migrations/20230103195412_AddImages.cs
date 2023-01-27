using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bc89c001-73af-4e53-9851-bb52be23d487"));

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "ProductId", "Url" },
                values: new object[,]
                {
                    { new Guid("156c541d-fa7b-40a5-981f-872e2ddf3ce4"), new Guid("c49c6475-b8c9-4cbe-92cb-c9424b07453d"), "/images/Products/image1.jpg" },
                    { new Guid("2cceb603-09c7-4094-b68d-60897b3408a8"), new Guid("dff9eb75-338d-4bec-acca-536af43f314b"), "/images/Products/image4.jpg" },
                    { new Guid("7ef36c5d-5fe6-459f-871b-49cca0d21be1"), new Guid("2fd73246-ad73-414f-9f29-c7820ec9c3a9"), "/images/Products/image3.jpg" },
                    { new Guid("c96dc613-1372-4746-87d7-47fed78a990b"), new Guid("399376e5-1ada-486f-acbf-033e6d6ba649"), "/images/Products/image2.jpg" },
                    { new Guid("eff77ecd-0c8d-4396-a3de-a047beb07a46"), new Guid("cd71401a-3492-451f-bceb-172e89327423"), "/images/Products/image5.jpg" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2fd73246-ad73-414f-9f29-c7820ec9c3a9"),
                column: "Cost",
                value: 25000m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("399376e5-1ada-486f-acbf-033e6d6ba649"),
                column: "Cost",
                value: 15000m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c49c6475-b8c9-4cbe-92cb-c9424b07453d"),
                columns: new[] { "Cost", "Name" },
                values: new object[] { 25000m, "Lacoste мужские" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cd71401a-3492-451f-bceb-172e89327423"),
                column: "Cost",
                value: 25000m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dff9eb75-338d-4bec-acca-536af43f314b"),
                column: "Cost",
                value: 25000m);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "IsFavorite", "Name" },
                values: new object[] { new Guid("735e2ff8-4a9d-417d-e28d-08daddc27f08"), 25000m, "Исключительно натуральные материалы", false, "Gucci для дома" });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "ProductId", "Url" },
                values: new object[] { new Guid("68bfe1d6-a659-4407-aa2a-d38b10af42b1"), new Guid("735e2ff8-4a9d-417d-e28d-08daddc27f08"), "/images/Products/image6.jpg" });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("735e2ff8-4a9d-417d-e28d-08daddc27f08"));

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2fd73246-ad73-414f-9f29-c7820ec9c3a9"),
                columns: new[] { "Cost", "ImagePath" },
                values: new object[] { 5000m, "/images/image3.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("399376e5-1ada-486f-acbf-033e6d6ba649"),
                columns: new[] { "Cost", "ImagePath" },
                values: new object[] { 25000m, "/images/image2.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c49c6475-b8c9-4cbe-92cb-c9424b07453d"),
                columns: new[] { "Cost", "ImagePath", "Name" },
                values: new object[] { 15000m, "/images/image1.jpg", "Туфли Lacoste" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cd71401a-3492-451f-bceb-172e89327423"),
                columns: new[] { "Cost", "ImagePath" },
                values: new object[] { 12000m, "/images/image5.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("dff9eb75-338d-4bec-acca-536af43f314b"),
                columns: new[] { "Cost", "ImagePath" },
                values: new object[] { 45000m, "/images/image4.jpg" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "ImagePath", "IsFavorite", "Name" },
                values: new object[] { new Guid("bc89c001-73af-4e53-9851-bb52be23d487"), 75000m, "Исключительно натуральные материалы", "/images/image6.jpg", false, "Guccci для дома" });
        }
    }
}
