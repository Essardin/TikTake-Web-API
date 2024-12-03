using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RobocopsWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class sixth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Users_UserProfileUserId",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_UserProfileUserId",
                table: "FriendRequests");

            migrationBuilder.DropColumn(
                name: "UserProfileUserId",
                table: "FriendRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserProfileUserId",
                table: "FriendRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_UserProfileUserId",
                table: "FriendRequests",
                column: "UserProfileUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Users_UserProfileUserId",
                table: "FriendRequests",
                column: "UserProfileUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
