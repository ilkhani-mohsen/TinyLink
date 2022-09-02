using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyLink.Persistence.SqlServer.Migrations
{
    public partial class AddLinkVisitEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkVisits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkId = table.Column<long>(type: "bigint", nullable: false),
                    VisitedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkVisits_Links_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkVisits_IsProcessed",
                table: "LinkVisits",
                column: "IsProcessed");

            migrationBuilder.CreateIndex(
                name: "IX_LinkVisits_LinkId",
                table: "LinkVisits",
                column: "LinkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkVisits");
        }
    }
}
