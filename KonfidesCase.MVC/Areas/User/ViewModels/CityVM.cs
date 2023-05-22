namespace KonfidesCase.MVC.Areas.User.ViewModels
{
    public class CityVM
    {
        #region Properties
        public int Id { get; set; }
        [DisplayName("Ad")]
        public string Name { get; set; } = string.Empty;
        #endregion
    }
}
