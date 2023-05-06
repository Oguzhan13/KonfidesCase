using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace KonfidesCase.ViewModel
{
    public class ChangePasswordVM
    {
        #region Properties
        [DisplayName("Geçerli Şifre")]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = string.Empty;
        [DisplayName("Yeni Şifre")]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
        #endregion
    }
}
