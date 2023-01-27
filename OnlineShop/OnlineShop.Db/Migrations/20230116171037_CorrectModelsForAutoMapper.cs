using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class CorrectModelsForAutoMapper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserDeliveryInfo_UserInfoId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UserInfoId",
                table: "Orders",
                newName: "UserDeliveryInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserInfoId",
                table: "Orders",
                newName: "IX_Orders_UserDeliveryInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserDeliveryInfo_UserDeliveryInfoId",
                table: "Orders",
                column: "UserDeliveryInfoId",
                principalTable: "UserDeliveryInfo",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserDeliveryInfo_UserDeliveryInfoId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UserDeliveryInfoId",
                table: "Orders",
                newName: "UserInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserDeliveryInfoId",
                table: "Orders",
                newName: "IX_Orders_UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserDeliveryInfo_UserInfoId",
                table: "Orders",
                column: "UserInfoId",
                principalTable: "UserDeliveryInfo",
                principalColumn: "Id");
        }
    }
}
