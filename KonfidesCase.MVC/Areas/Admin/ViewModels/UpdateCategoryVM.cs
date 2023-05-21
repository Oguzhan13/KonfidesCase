using System.ComponentModel;

namespace KonfidesCase.MVC.Areas.Admin.ViewModels
{
    public class UpdateCategoryVM
    {
        public int Id { get; set; }
        [DisplayName("Ad")]
        public string Name { get; set; } = string.Empty;
    }
}
