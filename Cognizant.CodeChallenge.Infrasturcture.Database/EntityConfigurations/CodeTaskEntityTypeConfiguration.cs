using Cognizant.CodeChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cognizant.CodeChallenge.Infrastructure.Database.EntityConfigurations
{
    class CodeTaskEntityTypeConfiguration : IEntityTypeConfiguration<CodeTask>
    {
        public void Configure(EntityTypeBuilder<CodeTask> builder)
        {
            builder.HasKey(ct => ct.Id);

            builder.Property(ct => ct.Name);
            builder.Property(ct => ct.Description);

            builder.OwnsMany(ct => ct.TestCases, onb =>
            {
                onb.ToTable("CodeTaskTestCases");
                onb.WithOwner().HasForeignKey("CodeTaskId");

                onb.Property(p => p.InputValue);
                onb.Property(p => p.OutputValue);
            });
        }
    }
}
