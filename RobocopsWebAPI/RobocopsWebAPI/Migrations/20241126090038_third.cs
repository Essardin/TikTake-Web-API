using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RobocopsWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Users_SelfUserId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "FriendRequestReceived",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "FriendsUserId",
                table: "Friends");

            migrationBuilder.RenameColumn(
                name: "SelfUserId",
                table: "Friends",
                newName: "UserBId");

            migrationBuilder.RenameColumn(
                name: "RequestApproval",
                table: "Friends",
                newName: "UserAId");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_SelfUserId",
                table: "Friends",
                newName: "IX_Friends_UserBId");

            migrationBuilder.CreateTable(
                name: "FriendRequests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FriendRequestReceived = table.Column<bool>(type: "bit", nullable: false),
                    RequestApproval = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserProfileUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_FriendRequests_Users_UserProfileUserId",
                        column: x => x.UserProfileUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_UserProfileUserId",
                table: "FriendRequests",
                column: "UserProfileUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Users_UserBId",
                table: "Friends",
                column: "UserBId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Users_UserBId",
                table: "Friends");

            migrationBuilder.DropTable(
                name: "FriendRequests");

            migrationBuilder.RenameColumn(
                name: "UserBId",
                table: "Friends",
                newName: "SelfUserId");

            migrationBuilder.RenameColumn(
                name: "UserAId",
                table: "Friends",
                newName: "RequestApproval");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_UserBId",
                table: "Friends",
                newName: "IX_Friends_SelfUserId");

            migrationBuilder.AddColumn<bool>(
                name: "FriendRequestReceived",
                table: "Friends",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FriendsUserId",
                table: "Friends",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Users_SelfUserId",
                table: "Friends",
                column: "SelfUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
