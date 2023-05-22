namespace KonfidesCase.MVC.Areas.Admin.ViewModels
{
    public class CategoryVM
    {
        #region Properties
        public int Id { get; set; }
        [DisplayName("Ad")]
        public string Name { get; set; } = string.Empty;
        #endregion
    }
}
