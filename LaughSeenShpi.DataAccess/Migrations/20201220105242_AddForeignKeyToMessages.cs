using Microsoft.EntityFrameworkCore.Migrations;

namespace LaughSeenShpi.DataAccess.Migrations
{
    public partial class AddForeignKeyToMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomMember",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "RoomMemberId",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RoomMemberId",
                table: "Messages",
                column: "RoomMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_RoomMembers_RoomMemberId",
                table: "Messages",
                column: "RoomMemberId",
                principalTable: "RoomMembers",
                principalColumn: "MemberID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_RoomMembers_RoomMemberId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RoomMemberId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "RoomMemberId",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "RoomMember",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
