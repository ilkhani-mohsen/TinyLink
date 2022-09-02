using TinyLink.Common.Dependency;
using TinyLink.Core.Domain.LinkVisits.Contracts;
using TinyLink.Core.Domain.LinkVisits.Entities;

namespace TinyLink.Persistence.SqlServer.LinkVisits
{
    public class LinkVisitsRepository : ILinkVistsRepository, IScopedLifetime
    {
        private readonly TinyLinkDbContext dbContext;

        public LinkVisitsRepository(TinyLinkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddLinkVist(LinkVisit linkVisit)
        {
            await dbContext.LinkVisits.AddAsync(linkVisit);
            await dbContext.SaveChangesAsync();
        }
    }
}
