namespace KonfidesCase.Entity.EntityTypeConfigurations
{
    public class AppUserActivityConfiguration : IEntityTypeConfiguration<AppUserActivity>
    {
        #region Fluent API
        public void Configure(EntityTypeBuilder<AppUserActivity> builder)
        {
            builder.ToTable("Kullanıcı-Etkinlik");

            builder.HasKey(ua => ua.Id);

            builder.Property(ua => ua.UserId).IsRequired()
                .HasColumnOrder(1)
                .HasColumnName("Kullanıcı Id");
            builder.Property(ua => ua.ActivityId).IsRequired()
                .HasColumnOrder(2)
                .HasColumnName("Etkinlik Id");

            builder.HasOne(ua => ua.Activity).WithMany(a => a.AttendedUsers).HasForeignKey(ua => ua.ActivityId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ua => ua.User).WithMany(a => a.Activities).HasForeignKey(ua => ua.UserId).OnDelete(DeleteBehavior.Restrict);
        }
        #endregion
    }
}
