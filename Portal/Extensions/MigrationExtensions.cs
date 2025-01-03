using Microsoft.EntityFrameworkCore;
using Portal.Models;
using System.Runtime.CompilerServices;

namespace Portal.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using PortalContext context = scope.ServiceProvider.GetRequiredService<PortalContext>();

            context.Database.Migrate();
        }
    }
}
