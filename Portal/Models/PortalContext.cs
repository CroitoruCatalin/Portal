using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Portal.Models
{
    public class PortalContext : IdentityDbContext<User>
    {
        public PortalContext(DbContextOptions<PortalContext> options) 
            : base(options)
        { 
        }
        public DbSet<Post> Posts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("portal");
        }
    }
}
