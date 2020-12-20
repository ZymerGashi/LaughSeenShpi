using Microsoft.EntityFrameworkCore.Migrations;

namespace LaughSeenShpi.DataAccess.Migrations
{
    public partial class AddForeignKeyToRoomMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RoomMembers_MemberRoomId",
                table: "RoomMembers",
                column: "MemberRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomMembers_Room_MemberRoomId",
                table: "RoomMembers",
                column: "MemberRoomId",
                principalTable: "Room",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomMembers_Room_MemberRoomId",
                table: "RoomMembers");

            migrationBuilder.DropIndex(
                name: "IX_RoomMembers_MemberRoomId",
                table: "RoomMembers");
        }
    }
}
