using TinyLink.Core.Domain.LinkVisitStatistics.Contracts;
using Microsoft.EntityFrameworkCore;
using TinyLink.Common.Dependency;

namespace TinyLink.Persistence.SqlServer.LinkVisitStatistics
{
    public class LinkVisitStatisticsRepository : ILinkVisitStatisticsRepository, IScopedLifetime
    {
        private readonly TinyLinkDbContext dbContext;

        public LinkVisitStatisticsRepository(TinyLinkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Sync()
        {
            await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.spLinkVisitStatisticsSync;");
        }
    }
}
