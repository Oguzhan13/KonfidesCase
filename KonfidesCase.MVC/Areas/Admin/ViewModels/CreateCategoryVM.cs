using System.ComponentModel;

namespace KonfidesCase.MVC.Areas.Admin.ViewModels
{
    public class CreateCategoryVM
    {
        [DisplayName("Ad")]
        public string Name { get; set; } = string.Empty;
    }
}
