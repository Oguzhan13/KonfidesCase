using System.Globalization;

namespace KonfidesCase.Authentication.Entities.Configurations
{
    public class AuthUserConfiguration : IEntityTypeConfiguration<AuthUser>
    {
        #region Constructor
        public AuthUserConfiguration()
        {            
            admin.NormalizedEmail = admin.Email!.ToUpper(CultureInfo.GetCultureInfo("en-US"));
            admin.UserName = admin.Email;
            admin.NormalizedUserName = admin.NormalizedEmail;
            admin.PasswordHash = new PasswordHasher<AuthUser>().HashPassword(admin, "Admin.123");
        }
        #endregion

        #region Seed Data
        public static AuthUser admin = new()
        {
            Email = "admin@example.com",
            FirstName = "Admin",
            LastName = "Manager",
        };
        #endregion

        #region Fluent API
        public void Configure(EntityTypeBuilder<AuthUser> builder)
        {            
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnOrder(1);
            builder.Property(u => u.Email).IsRequired()
                .HasColumnOrder(2)
                .HasColumnName("Mail Adresi");
            builder.Property(u => u.FirstName).IsRequired()
                .HasColumnOrder(3)
                .HasColumnName("Ad");
            builder.Property(u => u.LastName).IsRequired()
                .HasColumnOrder(4)
                .HasColumnName("Soyad");
            builder.Property(u => u.PasswordHash).IsRequired()
                .HasColumnOrder(5)
                .HasColumnName("Şifrelenmiş Şifre");

            builder.HasIndex(u => u.NormalizedEmail).IsUnique();
            builder.HasIndex(u => u.NormalizedUserName).IsUnique();
                        
            builder.HasData(admin);
        }
        #endregion
    }
}
