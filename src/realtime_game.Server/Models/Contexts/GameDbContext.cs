using Microsoft.EntityFrameworkCore;
using realtime_game.Shared.Models.Contexts;

namespace realtime_game.Server.Models.Contexts
{
    public class GameDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

#if DEBUG
        readonly string connectionString = "server=localhost;port=3306;database=realtime_game;user=jobi;password=jobi;";
# else
        readonly string connectionString = "server=db-ge0202400.mysql.database.azure.com;port=3306;database=realtime_game241203;user=student;password=Yoshidajobi2024;SslMode=Required;";
# endif


        //readonly string connectionString = "server=localhost;database=realtime_game;user=jobi;password=jobi;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString,
                new MySqlServerVersion(new Version(8, 0)));
        }
    }
}
