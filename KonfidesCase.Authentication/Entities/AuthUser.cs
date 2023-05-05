namespace KonfidesCase.Authentication.Entities
{
    public class AuthUser : IdentityUser<Guid>
    {
        #region Constructor
        public AuthUser()
        {
            Id = Guid.NewGuid();
            EmailConfirmed = true;
            ConcurrencyStamp = Guid.NewGuid().ToString();
            SecurityStamp = Guid.NewGuid().ToString();
            LockoutEnabled = true;
            LockoutEnd = DateTime.Now.AddMinutes(5);
        }
        #endregion

        #region Properties
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        #endregion
    }
}
