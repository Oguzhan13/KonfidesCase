namespace KonfidesCase.Authentication.Entities.Configurations
{
    public class AuthRoleConfiguration : IEntityTypeConfiguration<AuthRole>
    {
        #region Constructor
        public AuthRoleConfiguration()
        {
            adminRole.NormalizedName = adminRole.Name!.ToUpper(CultureInfo.GetCultureInfo("en-US"));
            userRole.NormalizedName = userRole.Name!.ToUpper(CultureInfo.GetCultureInfo("en-US"));
        }
        #endregion

        #region Seed Datas
        public static AuthRole adminRole = new()
        {
            Name = "admin",
        };
        public static AuthRole userRole = new()
        {
            Name = "user",
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
