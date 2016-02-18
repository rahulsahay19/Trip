using Microsoft.Data.Entity;

namespace WorldTrip.Models
{
    public class TripContext :DbContext
    {
        public TripContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<WorldTrip.Models.Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Startup.ConfigurationBuilder["Data:TripContextConnection"];
            optionsBuilder.UseSqlServer(connString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
