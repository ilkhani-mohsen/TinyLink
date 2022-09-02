using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyLink.Core.Domain.LinkVisits.Entities;

namespace TinyLink.Persistence.SqlServer.LinkVisits.TypeConfigurations
{
    internal class LinkVisitTypeConfiguration : IEntityTypeConfiguration<LinkVisit>
    {
        public const string TableName = "LinkVisits";
        public void Configure(EntityTypeBuilder<LinkVisit> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.IsProcessed);
        }
    }
}
