namespace KonfidesCase.Authentication.Entities.Configurations
{
    public class AuthRoleConfiguration : IEntityTypeConfiguration<AuthRole>
    {
        #region Seed Datas
        public static AuthRole adminRole = new()
        {
            Id = Guid.Parse("a6ef0654-a9c5-4085-8581-673e702c0ad4"),
            Name = "admin",
            NormalizedName = "admin".ToUpper(CultureInfo.GetCultureInfo("en-US"))
    };
        public static AuthRole userRole = new()
        {
            Id = Guid.Parse("ffbaa166-158e-4254-83df-ee7d13db3749"),
            Name = "user",
            NormalizedName = "user".ToUpper(CultureInfo.GetCultureInfo("en-US")),            
        };
        #endregion

        #region Fluent API
        public void Configure(EntityTypeBuilder<AuthRole> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id).ValueGeneratedOnAdd()
                .HasColumnOrder(1);
            builder.Property(r => r.Name).IsRequired()
                .HasColumnOrder(2);
                        
            builder.HasData(adminRole,userRole);
        }
        #endregion
    }
}
