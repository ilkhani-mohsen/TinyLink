namespace TinyLink.Common.Core.Domain.Entities
{
    public interface Entity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
