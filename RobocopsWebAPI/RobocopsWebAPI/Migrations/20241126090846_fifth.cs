using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RobocopsWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserBId",
                table: "Friends",
                newName: "FriendsUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FriendsUserId",
                table: "Friends",
                newName: "UserBId");
        }
    }
}
