using Application.CodeChallenge.Domain.Entities;
using Application.CodeChallenge.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Application.CodeChallenge.Infrastructure.Database.EntityConfigurations
{
    internal class SolutionEntityTypeConfiguration : IEntityTypeConfiguration<Solution>
    {
        public void Configure(EntityTypeBuilder<Solution> builder)
        {
            builder.HasKey(s => s.Id);
            
            builder.Property(s => s.Code);
            builder.Property(s => s.LanguageName);
            builder.Property(s => s.Status).HasConversion(new EnumToStringConverter<Status>());
            builder.HasOne(s => s.Task).WithMany();
        }
    }
}
