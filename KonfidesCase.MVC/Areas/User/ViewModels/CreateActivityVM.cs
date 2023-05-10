namespace KonfidesCase.MVC.Areas.User.ViewModels
{
    public class CreateActivityVM
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ActivityDate { get; set; }
        public int Quota { get; set; }
        public string Address { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int CityId { get; set; }

        public ICollection<CategoryVM>? Categories { get; set; }
        public ICollection<CityVM>? Cities { get; set; }
    }
}
