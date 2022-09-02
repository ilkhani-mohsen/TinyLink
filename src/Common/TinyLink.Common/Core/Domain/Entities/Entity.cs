namespace TinyLink.Common.Core.Domain.Entities
{
    public abstract class Entity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
