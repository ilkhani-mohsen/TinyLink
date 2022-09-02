namespace TinyLink.Common.Core.Domain.Entities
{
    public abstract class Entity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}
