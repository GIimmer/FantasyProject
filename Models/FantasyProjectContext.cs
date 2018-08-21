using Microsoft.EntityFrameworkCore;

namespace FantasyProject.Models
{
    public class FantasyProjectContext : DbContext
    {
        public FantasyProjectContext(DbContextOptions<FantasyProjectContext> options) : base(options) { }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}