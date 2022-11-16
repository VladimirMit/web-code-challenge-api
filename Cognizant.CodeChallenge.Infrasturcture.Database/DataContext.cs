using Application.CodeChallenge.Domain.Entities;
using Application.CodeChallenge.Infrastructure.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Application.CodeChallenge.Infrastructure.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        
        //TODO fixnaming
        public DbSet<CodeTask> CodeTask { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ParticipantEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CodeTaskEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SolutionEntityTypeConfiguration());
        }
    }
}
