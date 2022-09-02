using TinyLink.Common.Core.Domain.Entities;
using TinyLink.Core.Domain.Links.Entities;

namespace TinyLink.Core.Domain.LinkVisits.Entities
{
    public class LinkVisit : Entity<long>
    {
        public long LinkId { get; set; }
        public DateTime VisitedAt { get; set; }
        public bool IsProcessed { get; set; }

        public Link Link { get; set; }
    }
}
