namespace KonfidesCase.MVC.Areas.Admin.ViewModels
{
    public class ActivityDetailVM
    {
        public Guid Id { get; set; }
        public string Organizer { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ActivityDate { get; set; }
        public int Quota { get; set; }
        public string Address { get; set; } = string.Empty;
        public bool? IsConfirm { get; set; }
        public string Category { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }
}
