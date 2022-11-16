using Application.CodeChallenge.Domain.Entities;
using Application.CodeChallenge.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Application.CodeChallenge.Infrastructure.Database.EntityConfigurations
{
    internal class CodeTaskEntityTypeConfiguration : IEntityTypeConfiguration<CodeTask>
    {
        public void Configure(EntityTypeBuilder<CodeTask> builder)
        {
            builder.HasKey(ct => ct.Id);

            builder.Property(ct => ct.Name);
            builder.Property(ct => ct.Description);
            builder.Property(ct => ct.InputType).HasConversion(new EnumToStringConverter<InputType>());

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
