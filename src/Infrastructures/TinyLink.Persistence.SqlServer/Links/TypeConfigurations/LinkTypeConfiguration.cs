using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyLink.Core.Domain.Links.Entities;

namespace TinyLink.Persistence.SqlServer.Links.TypeConfigurations
{
    public class LinkTypeConfiguration : IEntityTypeConfiguration<Link>
    {
        private const string TableName = "Links";
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Url)
                .IsRequired()
                .HasMaxLength(2048);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasIndex(x => x.Code)
                .IsUnique();
        }
    }
}
