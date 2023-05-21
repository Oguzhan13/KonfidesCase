using System.ComponentModel;

namespace KonfidesCase.MVC.Areas.Admin.ViewModels
{
    public class CityVM
    {
        public int Id { get; set; }
        [DisplayName("Ad")]
        public string Name { get; set; } = string.Empty;
    }
}
