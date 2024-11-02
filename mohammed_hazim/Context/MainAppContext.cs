using Microsoft.EntityFrameworkCore;
using mohammed_hazim.Model;

namespace mohammed_hazim.Context
{
    public class MainAppContext : DbContext
    {

        public DbSet<Cars> Cars { get; set; }
        public MainAppContext(DbContextOptions<MainAppContext> contextOptions) : base(contextOptions)
        {

        }
    }
}
