using Microsoft.EntityFrameworkCore.Migrations;

namespace LaughSeenShpi.DataAccess.Migrations
{
    public partial class AddSocketIdColumnToMessagesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "socketId",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "socketId",
                table: "Messages");
        }
    }
}
