using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorWebRtc_Persistence.Migrations
{
    /// <inheritdoc />
    public partial class userFriendConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserUserFriend");

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePicture",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "ReceiverUserId",
                table: "UserFriends",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RequesterId",
                table: "UserFriends",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserFriends",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_ReceiverUserId",
                table: "UserFriends",
                column: "ReceiverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_RequesterId",
                table: "UserFriends",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_UserId",
                table: "UserFriends",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_Users_ReceiverUserId",
                table: "UserFriends",
                column: "ReceiverUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_Users_RequesterId",
                table: "UserFriends",
                column: "RequesterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_Users_UserId",
                table: "UserFriends",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_Users_ReceiverUserId",
                table: "UserFriends");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_Users_RequesterId",
                table: "UserFriends");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_Users_UserId",
                table: "UserFriends");

            migrationBuilder.DropIndex(
                name: "IX_UserFriends_ReceiverUserId",
                table: "UserFriends");

            migrationBuilder.DropIndex(
                name: "IX_UserFriends_RequesterId",
                table: "UserFriends");

            migrationBuilder.DropIndex(
                name: "IX_UserFriends_UserId",
                table: "UserFriends");

            migrationBuilder.DropColumn(
                name: "ReceiverUserId",
                table: "UserFriends");

            migrationBuilder.DropColumn(
                name: "RequesterId",
                table: "UserFriends");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserFriends");

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePicture",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserUserFriend",
                columns: table => new
                {
                    FriendsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUserFriend", x => new { x.FriendsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserUserFriend_UserFriends_FriendsId",
                        column: x => x.FriendsId,
                        principalTable: "UserFriends",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUserFriend_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserUserFriend_UsersId",
                table: "UserUserFriend",
                column: "UsersId");
        }
    }
}
