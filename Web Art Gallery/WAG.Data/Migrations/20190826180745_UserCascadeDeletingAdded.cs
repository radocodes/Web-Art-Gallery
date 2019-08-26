using Microsoft.EntityFrameworkCore.Migrations;

namespace WAG.Data.Migrations
{
    public partial class UserCascadeDeletingAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_WAGUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactMessages_AspNetUsers_WAGUserId",
                table: "ContactMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_WAGUserId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_WAGUserId",
                table: "Comments",
                column: "WAGUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactMessages_AspNetUsers_WAGUserId",
                table: "ContactMessages",
                column: "WAGUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_WAGUserId",
                table: "Orders",
                column: "WAGUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_WAGUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactMessages_AspNetUsers_WAGUserId",
                table: "ContactMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_WAGUserId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_WAGUserId",
                table: "Comments",
                column: "WAGUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactMessages_AspNetUsers_WAGUserId",
                table: "ContactMessages",
                column: "WAGUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_WAGUserId",
                table: "Orders",
                column: "WAGUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
