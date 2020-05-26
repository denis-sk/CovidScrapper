using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class CoronaCountryConfiguration : IEntityTypeConfiguration<CoronaCountry>
    {
        public void Configure(EntityTypeBuilder<CoronaCountry> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(t => t.NewCases)
                .HasColumnType("decimal(18,2)");
            builder.Property(t => t.NewDeaths)
               .HasColumnType("decimal(18,2)");
            builder.Property(t => t.TotalCases)
               .HasColumnType("decimal(18,2)");
            builder.Property(t => t.TotalDeaths)
               .HasColumnType("decimal(18,2)");
            builder.Property(t => t.TotalRecovered)
               .HasColumnType("decimal(18,2)");
        }
    }
}
