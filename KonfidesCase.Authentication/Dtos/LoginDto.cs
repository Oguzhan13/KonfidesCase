namespace KonfidesCase.Authentication.Dtos
{
    public class LoginDto
    {
        #region Properties
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        #endregion
    }
}
