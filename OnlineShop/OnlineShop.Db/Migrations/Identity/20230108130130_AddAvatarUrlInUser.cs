using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Db.Migrations.Identity
{
    /// <inheritdoc />
    public partial class AddAvatarUrlInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarImageUrl",
                table: "AspNetUsers");
        }
    }
}
