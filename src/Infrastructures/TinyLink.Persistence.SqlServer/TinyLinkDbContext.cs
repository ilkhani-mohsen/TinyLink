using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TinyLink.Core.Domain.Links.Entities;
using TinyLink.Core.Domain.LinkVisits.Entities;
using TinyLink.Core.Domain.LinkVisitStatistics.Entities;

namespace TinyLink.Persistence.SqlServer
{
    public class TinyLinkDbContext : DbContext
    {
        public DbSet<Link> Links { get; set; }
        public DbSet<LinkVisit> LinkVisits { get; set; }
        public DbSet<LinkVisitStatistic>  LinkVisitStatistics { get; set; }

        public TinyLinkDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
