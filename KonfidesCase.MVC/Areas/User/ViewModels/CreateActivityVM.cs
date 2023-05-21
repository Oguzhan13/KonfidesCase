using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KonfidesCase.MVC.Areas.User.ViewModels
{
    public class CreateActivityVM
    {
        [DisplayName("Ad")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("Açıklama")]
        public string Description { get; set; } = string.Empty;
        [DisplayName("Etkinlik Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime ActivityDate { get; set; }
        [DisplayName("Kontenjan")]
        public int Quota { get; set; }
        [DisplayName("Adres")]
        public string Address { get; set; } = string.Empty;
        [DisplayName("Kategori Id")]
        public int CategoryId { get; set; }
        [DisplayName("Şehir Id")]
        public int CityId { get; set; }

        public ICollection<CategoryVM>? Categories { get; set; }
        public ICollection<CityVM>? Cities { get; set; }
    }
}
