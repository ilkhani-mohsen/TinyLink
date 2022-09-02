using TinyLink.Core.Domain.LinkVisits.Entities;

namespace TinyLink.Core.Domain.LinkVisits.Contracts
{
    public interface ILinkVisitsLogStrategy
    {
        Task Log(LinkVisit linkVisit);
    }
}
