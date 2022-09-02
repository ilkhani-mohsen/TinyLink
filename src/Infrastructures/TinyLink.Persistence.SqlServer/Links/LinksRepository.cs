using Microsoft.EntityFrameworkCore;
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

        public async Task<Link> GetLinkByCode(string code)
        {
            return await dbContext.Links.FirstOrDefaultAsync(x => x.Code == code);
        }
    }
}
