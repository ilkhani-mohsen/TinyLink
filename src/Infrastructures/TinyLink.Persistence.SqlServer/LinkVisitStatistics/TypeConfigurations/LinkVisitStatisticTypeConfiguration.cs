using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyLink.Core.Domain.LinkVisitStatistics.Entities;

namespace TinyLink.Persistence.SqlServer.LinkVisitStatistics.TypeConfigurations
{
    internal class LinkVisitStatisticTypeConfiguration : IEntityTypeConfiguration<LinkVisitStatistic>
    {
        public const string TableName = "LinkVisitStatistics";
        public void Configure(EntityTypeBuilder<LinkVisitStatistic> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.LinkId, x.Year, x.Month, x.Day, x.Hour })
                .IsUnique();
        }
    }
}
