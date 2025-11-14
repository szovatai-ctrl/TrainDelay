using FelevesFeladat.Model;
using Microsoft.EntityFrameworkCore;

namespace FelevesFeladat.Persistence
{
    public class TrainDbContext : DbContext
    {
        public DbSet<Train> Trains { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Journey> Journeys { get; set; }

        public TrainDbContext(DbContextOptions<TrainDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Journey>()
                .Property(j => j.PassengerIds)
                .HasConversion(
                    v => string.Join(",", v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
                );
        }
    }
}