using KonfidesCase.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KonfidesCase.Entity.EntityTypeConfigurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        #region Fluent API
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Şehirler");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnOrder(1);
            builder.Property(c => c.Name).IsRequired()
                .HasColumnOrder (2)
                .HasColumnName("Ad");
        }
        #endregion
    }
}
