namespace KonfidesCase.MVC.Areas.User.ViewModels
{
    public class ChangePasswordVM
    {
        #region Properties        
        public string CurrentPassword { get; set; } = string.Empty;        
        public string NewPassword { get; set; } = string.Empty;
        #endregion
    }
}
