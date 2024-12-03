using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RobocopsWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FriendRequestReceived",
                table: "Friends",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RequestApproval",
                table: "Friends",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendRequestReceived",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "RequestApproval",
                table: "Friends");
        }
    }
}
