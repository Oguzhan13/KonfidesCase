using System.ComponentModel;

namespace KonfidesCase.MVC.Areas.User.ViewModels
{
    public class CancelActivityVM
    {
        public Guid Id { get; set; }
        [DisplayName("Ad")]
        public string Name { get; set; } = "Default Value";

    }
}
