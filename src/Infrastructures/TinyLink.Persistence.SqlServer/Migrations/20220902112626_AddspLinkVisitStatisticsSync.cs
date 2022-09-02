using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyLink.Persistence.SqlServer.Migrations
{
    public partial class AddspLinkVisitStatisticsSync : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var script =
"IF OBJECT_ID('spLinkVisitStatisticsSync', 'P') IS NOT NULL " +
"	DROP PROC spLinkVisitStatisticsSync ";

			migrationBuilder.Sql(script);

script =
"CREATE PROCEDURE spLinkVisitStatisticsSync " +
"AS " +
"BEGIN " +
"	BEGIN TRY " +
" " +
"		DECLARE @tbl TABLE(LinkId BIGINT, VisitedAt DATETIME2(7)); " +
"		DECLARE @tbl_grouped TABLE(LinkId BIGINT, Y INT, M INT, D INT, H INT, VisitCount INT, LinkVisitStatisticsId BIGINT); " +
" " +
" " +
"		BEGIN TRAN " +
" " +
"		UPDATE LinkVisits " +
"			SET IsProcessed = 1 " +
"			OUTPUT INSERTED.LinkId, INSERTED.VisitedAt INTO @tbl " +
"			WHERE IsProcessed = 0; " +
" " +
"		;WITH A AS( " +
"			SELECT LinkId, FORMAT(VisitedAt, N'yyyy') Y, FORMAT(VisitedAt, N'MM') M, FORMAT(VisitedAt, N'dd') D, FORMAT(VisitedAt, N'HH') H " +
"				FROM LinkVisits " +
"		) " +
"		INSERT INTO @tbl_grouped(LinkId, Y, M, D, H, VisitCount) " +
"			SELECT LinkId, Y, M, D, H, COUNT(0) " +
"				FROM A " +
"				GROUP BY LinkId, Y, M, D, H; " +
" " +
"		UPDATE TG " +
"			SET TG.LinkVisitStatisticsId = LVS.Id " +
"			FROM @tbl_grouped TG " +
"				INNER JOIN LinkVisitStatistics LVS ON(LVS.LinkId = TG.LinkId AND LVS.[Year] = TG.Y AND LVS.[Month] = TG.M AND LVS.[Day] = TG.D AND LVS.[Hour] = TG.H); " +
" " +
"		UPDATE LVS " +
"			SET LVS.VisitCount = LVS.VisitCount + TG.VisitCount " +
"			FROM LinkVisitStatistics LVS " +
"				INNER JOIN " +
"				@tbl_grouped TG ON(TG.LinkVisitStatisticsId IS NOT NULL AND LVS.Id = TG.LinkVisitStatisticsId); " +
" " +
"		INSERT INTO LinkVisitStatistics(LinkId, [Year], [Month], [Day], [Hour], [VisitCount]) " +
"			SELECT LinkId, Y, M, D, H, VisitCount FROM @tbl_grouped WHERE LinkVisitStatisticsId IS NULL; " +
" " +
"		; WITH A AS( " +
"			 SELECT LinkId, COUNT(0) VisitCount " +
"				 FROM @tbl " +
"				 GROUP BY LinkId " +
"		) " +
"		UPDATE L " +
"			SET L.TotalVistCount = L.TotalVistCount + A.VisitCount " +
"			FROM Links L " +
"				INNER JOIN A ON(L.Id = A.LinkId); " +
" " +
"		COMMIT; " +
" " +
"	END TRY " +
"	BEGIN CATCH " +
"		IF @@TRANCOUNT > 0 " +
"			ROLLBACK; " +
"	END CATCH " +
"END ";
			migrationBuilder.Sql(script);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var script =
"IF OBJECT_ID('spLinkVisitStatisticsSync', 'P') IS NOT NULL " +
"	DROP PROC spLinkVisitStatisticsSync ";

            migrationBuilder.Sql(script);
        }
    }
}
