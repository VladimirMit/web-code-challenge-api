using Cognizant.CodeChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cognizant.CodeChallenge.Infrastructure.Database.EntityConfigurations
{
    class ParticipantEntityTypeConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserName);
            builder.HasMany(p => p.Solutions).WithOne(s => s.Participant);
        }
    }
}
