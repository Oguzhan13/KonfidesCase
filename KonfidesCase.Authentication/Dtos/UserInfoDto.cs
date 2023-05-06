namespace KonfidesCase.Authentication.Dtos
{
    public class UserInfoDto
    {
        #region Properties
        public Guid Id { get; set; }        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<string> RoleNames { get; set; } = new HashSet<string>();
        public string Password { get; set; } = string.Empty;
        #endregion
    }
}
