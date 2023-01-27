using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdInOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserDeliveryInfo_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserInfoId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserInfoId",
                table: "Orders",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserDeliveryInfo_UserInfoId",
                table: "Orders",
                column: "UserInfoId",
                principalTable: "UserDeliveryInfo",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserDeliveryInfo_UserInfoId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserInfoId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserDeliveryInfo_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "UserDeliveryInfo",
                principalColumn: "Id");
        }
    }
}
