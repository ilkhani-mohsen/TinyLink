using TinyLink.Core.Domain.LinkVisits.Entities;

namespace TinyLink.Core.Domain.LinkVisits.Contracts
{
    public interface ILinkVistsRepository
    {
        Task AddLinkVist(LinkVisit linkVisit);
    }
}
