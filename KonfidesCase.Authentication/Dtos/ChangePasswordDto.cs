namespace KonfidesCase.Authentication.Dtos
{
    public class ChangePasswordDto
    {
        #region Properties
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;
        [Required]
        public string NewPassword { get; set; } = string.Empty;
        #endregion
    }
}
