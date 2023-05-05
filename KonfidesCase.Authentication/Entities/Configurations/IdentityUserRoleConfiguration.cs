namespace KonfidesCase.Authentication.Entities.Configurations
{
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        #region Seed Data
        public static IdentityUserRole<Guid> adminUserRole = new()
        {
            UserId = AuthUserConfiguration.admin.Id,
            RoleId = AuthRoleConfiguration.adminRole.Id
        };
        #endregion

        #region Fluent API
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder.HasData(adminUserRole);
        }
        #endregion
    }
}
