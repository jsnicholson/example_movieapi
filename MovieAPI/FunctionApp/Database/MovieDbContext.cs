using FunctionApp.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FunctionApp.Database {
    public class MovieDbContext : DbContext {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }

        public DbSet<Movie> movies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<Movie>()
                .HasKey(m => new { m.Release_Date, m.Title });
        }
    }
}