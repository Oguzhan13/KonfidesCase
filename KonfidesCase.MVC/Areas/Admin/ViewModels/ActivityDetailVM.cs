namespace KonfidesCase.MVC.Areas.Admin.ViewModels
{
    public class ActivityDetailVM
    {
        #region Properties
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
        [DisplayName("Kategori")]
        public string Category { get; set; } = string.Empty;
        [DisplayName("Şehir")]
        public string City { get; set; } = string.Empty;
        #endregion
    }
}
