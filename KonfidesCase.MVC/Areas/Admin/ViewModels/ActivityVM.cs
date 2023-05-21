using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KonfidesCase.MVC.Areas.Admin.ViewModels
{
    public class ActivityVM
    {
        public Guid Id { get; set; }
        [DisplayName("Organizatör")]
        public string Organizer { get; set; } = string.Empty;
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
        [DisplayName("Onay")]
        public bool? IsConfirm { get; set; }
        [DisplayName("Kategori Id")]
        public int CategoryId { get; set; }
        [DisplayName("Şehir Id")]
        public int CityId { get; set; }
    }
}
