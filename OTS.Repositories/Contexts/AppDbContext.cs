using OTS.Models;
using Microsoft.EntityFrameworkCore;

namespace OTS.Repositories.Contexts
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<Lookup> Lookups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lookup>().ToTable("SearchEngines");
            modelBuilder.Entity<Lookup>().HasData(MockDatabase.SearchEngines);
        }
    }
}
