using KonfidesCase.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KonfidesCase.Entity.EntityTypeConfigurations
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        #region Fluetn API
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Etkinlikler");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).IsRequired()
                .HasColumnOrder(1);
            builder.Property(a => a.Organizer).IsRequired()
                .HasColumnOrder(2)
                .HasColumnName("Organizatör");
            builder.Property(a => a.Name).IsRequired()
                .HasColumnOrder(3)
                .HasColumnName("Ad");
            builder.Property(a => a.Description).IsRequired()
                .HasColumnOrder(4)
                .HasColumnName("Açıklama");
            builder.Property(a => a.ActivityDate).IsRequired()
                .HasColumnOrder(5)
                .HasColumnName("Etkinlik Tarihi");
            builder.Property(a => a.Quota).IsRequired()
                .HasColumnOrder(6)
                .HasColumnName("Kontenjan");
            builder.Property(a => a.Address).IsRequired()
                .HasColumnOrder(7)
                .HasColumnName("Adres");
            builder.Property(a => a.IsConfirm).IsRequired(false)
                .HasColumnOrder(8)
                .HasColumnName("Onay");
            builder.Property(a => a.CategoryId).IsRequired(false)
                .HasColumnOrder(9)
                .HasColumnName("Kategori Id");
            builder.Property(a => a.CityId).IsRequired(false)
                .HasColumnOrder(10)
                .HasColumnName("Şehir Id");

            builder.HasOne(a => a.Category).WithMany(c => c.Activities).HasForeignKey(a => a.CategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.City).WithMany(c => c.Activities).HasForeignKey(a => a.CityId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(a => a.Tickets).WithOne(t => t.Activity).HasForeignKey(t => t.ActivityId).OnDelete(DeleteBehavior.Restrict);
        }
        #endregion
    }
}
