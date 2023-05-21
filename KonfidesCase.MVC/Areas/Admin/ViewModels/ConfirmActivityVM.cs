using System.ComponentModel;

namespace KonfidesCase.MVC.Areas.Admin.ViewModels
{
    public class ConfirmActivityVM
    {
        public Guid Id { get; set; }
        [DisplayName("Onay")]
        public bool? IsConfirm { get; set; }
    }
}
