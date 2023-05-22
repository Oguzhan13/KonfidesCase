namespace KonfidesCase.MVC.Areas.Admin.ViewModels
{
    public class ConfirmActivityVM
    {
        #region Properties
        public Guid Id { get; set; }
        [DisplayName("Onay")]
        public bool? IsConfirm { get; set; }
        #endregion
    }
}
