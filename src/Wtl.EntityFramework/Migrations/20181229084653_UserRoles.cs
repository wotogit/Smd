using Microsoft.EntityFrameworkCore.Migrations;

namespace Wtl.EntityFramework.Migrations
{
    public partial class UserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SmdUserRole_SmdUsers_UserId",
                table: "SmdUserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SmdUserRole",
                table: "SmdUserRole");

            migrationBuilder.RenameTable(
                name: "SmdUserRole",
                newName: "SmdUserRoles");

            migrationBuilder.RenameIndex(
                name: "IX_SmdUserRole_UserId",
                table: "SmdUserRoles",
                newName: "IX_SmdUserRoles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SmdUserRole_RoleId",
                table: "SmdUserRoles",
                newName: "IX_SmdUserRoles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmdUserRoles",
                table: "SmdUserRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SmdUserRoles_SmdUsers_UserId",
                table: "SmdUserRoles",
                column: "UserId",
                principalTable: "SmdUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SmdUserRoles_SmdUsers_UserId",
                table: "SmdUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SmdUserRoles",
                table: "SmdUserRoles");

            migrationBuilder.RenameTable(
                name: "SmdUserRoles",
                newName: "SmdUserRole");

            migrationBuilder.RenameIndex(
                name: "IX_SmdUserRoles_UserId",
                table: "SmdUserRole",
                newName: "IX_SmdUserRole_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SmdUserRoles_RoleId",
                table: "SmdUserRole",
                newName: "IX_SmdUserRole_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SmdUserRole",
                table: "SmdUserRole",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SmdUserRole_SmdUsers_UserId",
                table: "SmdUserRole",
                column: "UserId",
                principalTable: "SmdUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
