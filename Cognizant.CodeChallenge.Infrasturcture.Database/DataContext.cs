using Cognizant.CodeChallenge.Domain.Entities;
using Cognizant.CodeChallenge.Infrastructure.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Cognizant.CodeChallenge.Infrastructure.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Solution> Solutions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ParticipantEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CodeTaskEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SolutionEntityTypeConfiguration());
        }
    }
}
