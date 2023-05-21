using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KonfidesCase.MVC.ViewModels
{
    public class UserInfoVM
    {
        #region Properties
        public Guid Id { get; set; }
        [DisplayName("Ad")]        
        public string FirstName { get; set; } = string.Empty;
        [DisplayName("Soyad")]        
        public string LastName { get; set; } = string.Empty;
        [DisplayName("Mail Adresi")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]        
        public string Email { get; set; } = string.Empty;
        [DisplayName("Role")]        
        public string RoleName { get; set; } = string.Empty;
        [DisplayName("Şifre")]
        [PasswordPropertyText(true)]
        [DataType(DataType.Password)]        
        public string Password { get; set; } = string.Empty;
        #endregion
    }
}
