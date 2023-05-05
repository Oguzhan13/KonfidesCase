namespace KonfidesCase.Authentication.Entities
{
    public class AuthRole : IdentityRole<Guid>
    {
        #region Constructor
        public AuthRole()
        {
            Id = Guid.NewGuid();
            ConcurrencyStamp = Guid.NewGuid().ToString();
        }
        #endregion
    }
}
