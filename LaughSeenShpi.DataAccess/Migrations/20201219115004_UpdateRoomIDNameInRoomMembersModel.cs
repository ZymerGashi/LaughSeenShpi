using Microsoft.EntityFrameworkCore.Migrations;

namespace LaughSeenShpi.DataAccess.Migrations
{
    public partial class UpdateRoomIDNameInRoomMembersModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomMembers_Room_RoomIdID",
                table: "RoomMembers");

            migrationBuilder.DropIndex(
                name: "IX_RoomMembers_RoomIdID",
                table: "RoomMembers");

            migrationBuilder.DropColumn(
                name: "RoomIdID",
                table: "RoomMembers");

            migrationBuilder.AddColumn<int>(
                name: "RoomID",
                table: "RoomMembers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomMembers_RoomID",
                table: "RoomMembers",
                column: "RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomMembers_Room_RoomID",
                table: "RoomMembers",
                column: "RoomID",
                principalTable: "Room",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomMembers_Room_RoomID",
                table: "RoomMembers");

            migrationBuilder.DropIndex(
                name: "IX_RoomMembers_RoomID",
                table: "RoomMembers");

            migrationBuilder.DropColumn(
                name: "RoomID",
                table: "RoomMembers");

            migrationBuilder.AddColumn<int>(
                name: "RoomIdID",
                table: "RoomMembers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomMembers_RoomIdID",
                table: "RoomMembers",
                column: "RoomIdID");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomMembers_Room_RoomIdID",
                table: "RoomMembers",
                column: "RoomIdID",
                principalTable: "Room",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
