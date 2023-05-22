namespace KonfidesCase.MVC.ViewModels
{
    public class LoginVM
    {
        #region Properties
        [DisplayName("Mail Adresi")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Mail adresi bilgisi zorunludur!")]
        public string Email { get; set; } = string.Empty;
        [DisplayName("Şifre")]
        [PasswordPropertyText(true)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre bilgisi zorunludur!")]
        public string Password { get; set; } = string.Empty;
        #endregion
    }
}
