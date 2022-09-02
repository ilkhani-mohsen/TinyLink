using TinyLink.Common.Core.Domain.Entities;
using TinyLink.Core.Domain.Links.Entities;

namespace TinyLink.Core.Domain.LinkVisitStatistics.Entities
{
    public class LinkVisitStatistic : Entity<long>
    {
        public long LinkId { get; set; }
        public short Year { get; set; }
        public byte Month { get; set; }
        public byte Day { get; set; }
        public byte Hour { get; set; }
        public int VisitCount { get; set; }

        public Link Link { get; set; }
    }
}
