using apiGQL.WApi.Models;
using Microsoft.EntityFrameworkCore;

namespace apiGQL.WApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Command> Commands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<Platform>()
            .HasMany(p => p.Commands)
            .WithOne(c => c.Platform!)
            .HasForeignKey(c => c.PlatformId);

            modelBuilder
            .Entity<Command>()
            .HasOne(c => c.Platform)
            .WithMany(c => c.Commands)
            .HasForeignKey(c => c.PlatformId);
        }
    }
}