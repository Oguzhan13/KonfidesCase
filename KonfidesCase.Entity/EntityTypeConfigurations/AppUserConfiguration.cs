using KonfidesCase.Authentication.Entities.Configurations;
using KonfidesCase.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KonfidesCase.Entity.EntityTypeConfigurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        #region Seed Data
        public static AppUser admin = new()
        {
            Id = AuthUserConfiguration.admin.Id,
            FirstName = AuthUserConfiguration.admin.FirstName,
            LastName = AuthUserConfiguration.admin.LastName,
            Email = AuthUserConfiguration.admin.Email!,
            RoleName = AuthRoleConfiguration.adminRole.Name!
        };
        #endregion

        #region Fluent API
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("Kullanıcılar");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).IsRequired()
                .HasColumnOrder(1);
            builder.Property(u => u.RoleName).IsRequired()
                .HasColumnOrder(2)
                .HasColumnName("Rol");
            builder.Property(u => u.FirstName).IsRequired()
                .HasColumnOrder(3)
                .HasColumnName("Ad");
            builder.Property(u => u.LastName).IsRequired()
                .HasColumnOrder(4)
                .HasColumnName("Soyad");
            builder.Property(u => u.Email).IsRequired()
                .HasColumnOrder(5)
                .HasColumnName("Mail Adresi");

            builder.HasMany(u => u.Tickets).WithOne(t => t.User).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasData(admin);
        }
        #endregion
    }
}
