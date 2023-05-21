using System.ComponentModel;

namespace KonfidesCase.MVC.Areas.User.ViewModels
{
    public class ChangePasswordVM
    {
        #region Properties        
        [DisplayName("Eski Şifre")]
        public string CurrentPassword { get; set; } = string.Empty;
        [DisplayName("Yeni Şifre")]
        public string NewPassword { get; set; } = string.Empty;
        #endregion
    }
}
