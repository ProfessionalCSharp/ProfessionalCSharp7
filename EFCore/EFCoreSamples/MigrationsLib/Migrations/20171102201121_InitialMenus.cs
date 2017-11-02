using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MigrationsLib.Migrations
{
    public partial class InitialMenus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mc");

            migrationBuilder.CreateTable(
                name: "MenuCards",
                schema: "mc",
                columns: table => new
                {
                    MenuCardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCards", x => x.MenuCardId);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                schema: "mc",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MenuCardId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuId);
                    table.ForeignKey(
                        name: "FK_Menus_MenuCards_MenuCardId",
                        column: x => x.MenuCardId,
                        principalSchema: "mc",
                        principalTable: "MenuCards",
                        principalColumn: "MenuCardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menus_MenuCardId",
                schema: "mc",
                table: "Menus",
                column: "MenuCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menus",
                schema: "mc");

            migrationBuilder.DropTable(
                name: "MenuCards",
                schema: "mc");
        }
    }
}
