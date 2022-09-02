using TinyLink.Core.Domain.Links.Entities;

namespace TinyLink.Core.Domain.Links.Contracts
{
    public interface ILinksRepository
    {
        public Task AddLink(Link link);
        public Task<Link> GetLinkByCode(string code);
    }
}
