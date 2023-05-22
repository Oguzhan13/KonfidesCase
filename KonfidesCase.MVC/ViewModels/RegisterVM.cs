namespace KonfidesCase.MVC.ViewModels
{
    public class RegisterVM
    {
        #region Properties
        [DisplayName("Ad")]
        [Required(ErrorMessage = "Ad bilgisi zorunludur!")]
        public string FirstName { get; set; } = string.Empty;
        [DisplayName("Soyad")]
        [Required(ErrorMessage = "Soyad bilgisi zorunludur!")]
        public string LastName { get; set; } = string.Empty;
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
