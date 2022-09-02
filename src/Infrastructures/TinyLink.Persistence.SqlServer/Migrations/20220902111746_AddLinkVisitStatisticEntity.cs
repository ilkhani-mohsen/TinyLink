using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyLink.Persistence.SqlServer.Migrations
{
    public partial class AddLinkVisitStatisticEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkVisitStatistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkId = table.Column<long>(type: "bigint", nullable: false),
                    Year = table.Column<short>(type: "smallint", nullable: false),
                    Month = table.Column<byte>(type: "tinyint", nullable: false),
                    Day = table.Column<byte>(type: "tinyint", nullable: false),
                    Hour = table.Column<byte>(type: "tinyint", nullable: false),
                    VisitCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkVisitStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkVisitStatistics_Links_LinkId",
                        column: x => x.LinkId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkVisitStatistics_LinkId_Year_Month_Day_Hour",
                table: "LinkVisitStatistics",
                columns: new[] { "LinkId", "Year", "Month", "Day", "Hour" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkVisitStatistics");
        }
    }
}
