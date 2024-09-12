using Microsoft.EntityFrameworkCore;

namespace Foci_WebApp.Models
{
    public class FociDbContext: DbContext
    {
        public FociDbContext(DbContextOptions<FociDbContext> options) : base(options)
        {
            
        }

        public DbSet<Meccs> Meccsek {  get; set; }
    }
}
