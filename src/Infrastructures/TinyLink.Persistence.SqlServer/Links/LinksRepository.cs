using TinyLink.Common.Dependency;
using TinyLink.Core.Domain.Links.Contracts;
using TinyLink.Core.Domain.Links.Entities;

namespace TinyLink.Persistence.SqlServer.Links
{
    public class LinksRepository : ILinksRepository, IScopedLifetime
    {
        private readonly TinyLinkDbContext dbContext;

        public LinksRepository(TinyLinkDbContext tinyLinkDbContext)
        {
            dbContext = tinyLinkDbContext;
        }
        public async Task AddLink(Link link)
        {
            await dbContext.Links.AddAsync(link);
            await dbContext.SaveChangesAsync();
        }
    }
}
