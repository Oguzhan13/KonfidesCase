namespace KonfidesCase.Authentication.Dtos
{
    public class UserInfoDto
    {
        #region Properties
        public Guid Id { get; set; }        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        #endregion
    }
}
