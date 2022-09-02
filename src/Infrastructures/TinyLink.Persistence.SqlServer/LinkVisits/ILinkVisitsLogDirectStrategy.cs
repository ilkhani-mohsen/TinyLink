using TinyLink.Common.Dependency;
using TinyLink.Core.Domain.LinkVisits.Contracts;
using TinyLink.Core.Domain.LinkVisits.Entities;

namespace TinyLink.Persistence.SqlServer.LinkVisits
{
    public class ILinkVisitsLogDirectStrategy : ILinkVisitsLogStrategy, IScopedLifetime
    {
        private readonly ILinkVistsRepository linkVisitsRepository;

        public ILinkVisitsLogDirectStrategy(ILinkVistsRepository linkVisitsRepository)
        {
            this.linkVisitsRepository = linkVisitsRepository;
        }
        public async Task Log(LinkVisit linkVisit)
        {
            await linkVisitsRepository.AddLinkVist(linkVisit);
        }
    }
}
