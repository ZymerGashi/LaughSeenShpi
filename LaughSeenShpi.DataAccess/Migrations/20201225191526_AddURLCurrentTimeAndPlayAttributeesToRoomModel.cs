using Microsoft.EntityFrameworkCore.Migrations;

namespace LaughSeenShpi.DataAccess.Migrations
{
    public partial class AddURLCurrentTimeAndPlayAttributeesToRoomModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CurrentTime",
                table: "Room",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "MovieUrl",
                table: "Room",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PlayTheMovie",
                table: "Room",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentTime",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "MovieUrl",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "PlayTheMovie",
                table: "Room");
        }
    }
}
