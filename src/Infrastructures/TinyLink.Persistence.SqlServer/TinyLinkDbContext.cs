using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TinyLink.Core.Domain.Links.Entities;

namespace TinyLink.Persistence.SqlServer
{
    public class TinyLinkDbContext : DbContext
    {
        public DbSet<Link> Links { get; set; }

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
