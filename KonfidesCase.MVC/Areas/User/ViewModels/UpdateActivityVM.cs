namespace KonfidesCase.MVC.Areas.User.ViewModels
{
    public class UpdateActivityVM
    {
        public Guid Id { get; set; }
        public int Quota { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
