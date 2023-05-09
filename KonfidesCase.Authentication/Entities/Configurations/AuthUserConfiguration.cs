namespace KonfidesCase.Authentication.Entities.Configurations
{
    public class AuthUserConfiguration : IEntityTypeConfiguration<AuthUser>
    {
        #region Constructor
        public AuthUserConfiguration()
        {
            admin.PasswordHash = new PasswordHasher<AuthUser>().HashPassword(admin, "Admin.123");
        }
        #endregion

        #region Seed Data
        public static AuthUser admin = new()
        {
            Id = Guid.Parse("6e7fe5c6-1444-474b-9b43-d078cd892237"),
            Email = "admin@example.com",
            FirstName = "Admin",
            LastName = "Manager",
            UserName = "admin@example.com",
            NormalizedEmail = "admin@example.com".ToUpper(CultureInfo.GetCultureInfo("en-US")),
            NormalizedUserName = "admin@example.com".ToUpper(CultureInfo.GetCultureInfo("en-US")),
            //NormalizedEmail = "ADMIN@EXAMPLE.COM",
            //NormalizedUserName = "ADMIN@EXAMPLE.COM",
        };
        #endregion

        #region Fluent API
        public void Configure(EntityTypeBuilder<AuthUser> builder)
        {            
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnOrder(1);
            builder.Property(u => u.UserName).IsRequired()
                .HasColumnOrder(2)
                .HasColumnName("Kullanıcı Adı");
            builder.Property(u => u.NormalizedUserName).IsRequired()
                .HasColumnOrder(3)
                .HasColumnName("Stadart Kullanıcı Adı");
            builder.Property(u => u.Email).IsRequired()
                .HasColumnOrder(4)
                .HasColumnName("Mail Adresi");
            builder.Property(u => u.NormalizedEmail).IsRequired()
                .HasColumnOrder(5)
                .HasColumnName("Standart Mail Adresi");
            builder.Property(u => u.EmailConfirmed).IsRequired()
                .HasColumnOrder(6)
                .HasColumnName("Onaylanmış Email");
            builder.Property(u => u.FirstName).IsRequired()
                .HasColumnOrder(7)
                .HasColumnName("Ad");
            builder.Property(u => u.LastName).IsRequired()
                .HasColumnOrder(8)
                .HasColumnName("Soyad");
            builder.Property(u => u.PasswordHash).IsRequired()
                .HasColumnOrder(9)
                .HasColumnName("Kriptolanmış Şifre");            
            builder.Property(u => u.PhoneNumber).IsRequired(false)
                .HasColumnOrder(10)
                .HasColumnName("Telefon Numarası");
            builder.Property(u => u.PhoneNumberConfirmed).IsRequired()
                .HasColumnOrder(11)
                .HasColumnName("Onaylanmış Telefon Numarası");
            builder.Property(u => u.LockoutEnabled)
                .HasColumnOrder(12);
            builder.Property(u => u.LockoutEnd)
                .HasColumnOrder(13);
            builder.Property(u => u.ConcurrencyStamp)
                .HasColumnOrder(14);
            builder.Property(u => u.SecurityStamp)
                .HasColumnOrder(15);
            builder.Property(u => u.TwoFactorEnabled)
                .HasColumnOrder(16);
            builder.Property(u => u.AccessFailedCount)
                .HasColumnOrder(17);

            builder.HasIndex(u => u.NormalizedEmail).IsUnique();
            builder.HasIndex(u => u.NormalizedUserName).IsUnique();
                        
            builder.HasData(admin);
        }
        #endregion
    }
}
