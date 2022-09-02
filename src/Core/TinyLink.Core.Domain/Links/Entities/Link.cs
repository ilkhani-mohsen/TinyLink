using TinyLink.Common.Core.Domain.Entities;

namespace TinyLink.Core.Domain.Links.Entities
{
    public class Link : Entity<long>
    {
        public string Url { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public long TotalVistCount { get; set; }
        public bool IsVisible { get; set; }
    }
}
