using Microsoft.EntityFrameworkCore;
using realtime_game.Shared.Models.Contexts;

namespace realtime_game.Server.Models.Contexts
{
    public class GameDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        readonly string connectionpString =
            "server=localhost;database=realtime_game;user=jobi;password=jobi;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionpString,
                new MySqlServerVersion(new Version(8, 0)));
        }
    }
}
