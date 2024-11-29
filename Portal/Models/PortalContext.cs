using Microsoft.EntityFrameworkCore;

namespace Portal.Models
{
    public class PortalContext : DbContext
    {
        public PortalContext(DbContextOptions<PortalContext> options) 
            : base(options)
        { 
        }
        public DbSet<Post> Posts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
