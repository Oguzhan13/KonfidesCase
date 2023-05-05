using KonfidesCase.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KonfidesCase.Entity.EntityTypeConfigurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        #region Fluetn API
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Biletler");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnOrder(1);
            builder.Property(t => t.TicketNo).IsRequired()
                .HasColumnOrder(2)
                .HasColumnName("Bilet Numarası");
            builder.Property(t => t.UserId).IsRequired()
                .HasColumnOrder(3);
            builder.Property(t => t.ActivityId).IsRequired()
                .HasColumnOrder(4);
        }
        #endregion
    }
}
