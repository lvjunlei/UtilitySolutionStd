using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Utility.Framework.Test.Migrations.SlaveDb
{
    public partial class ljl20190308 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RoleId",
                table: "SysUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SysRole",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRole", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SysUsers_RoleId",
                table: "SysUsers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_SysUsers_SysRole_RoleId",
                table: "SysUsers",
                column: "RoleId",
                principalTable: "SysRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SysUsers_SysRole_RoleId",
                table: "SysUsers");

            migrationBuilder.DropTable(
                name: "SysRole");

            migrationBuilder.DropIndex(
                name: "IX_SysUsers_RoleId",
                table: "SysUsers");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "SysUsers");
        }
    }
}
