using KonfidesCase.Authentication.Entities;
using KonfidesCase.Authentication.Entities.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KonfidesCase.Authentication.DataAccess.Contexts
{
    public class KonfidesCaseAuthDbContext : IdentityDbContext<AuthUser, AuthRole, Guid>
    {
        #region Field & Constructor
        internal const string ConnectionName = "DefaultIdentity";
        public KonfidesCaseAuthDbContext(DbContextOptions<KonfidesCaseAuthDbContext> options) : base(options)
        {
            
        }
        #endregion

        #region OnModelCreating Method
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AuthUserConfiguration());
            builder.ApplyConfiguration(new AuthRoleConfiguration());
            builder.ApplyConfiguration(new IdentityUserRoleConfiguration());

            base.OnModelCreating(builder);
        }
        #endregion
    }
}
